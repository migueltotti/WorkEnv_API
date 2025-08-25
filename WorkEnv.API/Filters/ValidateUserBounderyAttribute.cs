using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WorkEnv.API.Filters;

public class ValidateUserBoundaryAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        // Get id from user claims
        var currentUserId = context.HttpContext.User
            .FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!Guid.TryParse(currentUserId, out var currentUserIdGuid))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        // Try to find id in different places.
        var targetUserId = GetTargetUserId(context, currentUserIdGuid);

        if (targetUserId.HasValue && currentUserIdGuid != targetUserId.Value)
        {
            context.Result = new ObjectResult(new ProblemDetails
            {
                Status = StatusCodes.Status403Forbidden,
                Title = "FORBIDDEN_USER_ACCESS",
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.3",
                Detail = "User doesn`t have permission to perform this action on a different user."
            })
            {
                StatusCode = 403
            };
        }
    }

    private Guid? GetTargetUserId(ActionExecutingContext context, Guid fallbackUserId)
    {
        // 1. Try get id from URL (userId parameter)
        if (context.ActionArguments.TryGetValue("userId", out var userIdObj))
        {
            if (Guid.TryParse(userIdObj?.ToString(), out var userId))
                return userId;
        }

        // 2. Try get id from body (search for common properties)
        foreach (var arg in context.ActionArguments.Values)
        {
            if (arg == null) continue;

            var type = arg.GetType();
            
            // Search for common properties that might have "userId"
            var userIdProperty = type.GetProperty("userId");

            if (userIdProperty?.PropertyType == typeof(Guid) || 
                userIdProperty?.PropertyType == typeof(Guid?))
            {
                var value = userIdProperty.GetValue(arg);
                if (value != null && Guid.TryParse(value.ToString(), out var id))
                    return id;
            }
        }

        // 3. If didnt find id, return null for fail
        return null;
    }
}