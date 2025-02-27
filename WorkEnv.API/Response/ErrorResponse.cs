using System.Net;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WorkEnv.Application.Result;

namespace WorkEnv.API.Response;

public static class ErrorResponse
{
    public static ActionResult ToProblemDetails(this Result result)
    {
        return result.Error.HttpStatusCode switch
        {
            HttpStatusCode.NotFound => new NotFoundObjectResult(new
                {
                    statusCode = (int?)result.Error.HttpStatusCode.Value,
                    title = result.Error.HttpStatusCode.Value.ToString(),
                    type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4",
                    extensions = new Dictionary<string, object?>
                    {
                        { "errors", new[] { result.Error } },
                        { "validationErrors", new[] { result.ValidationResults } }
                    }
                }
            ),
            HttpStatusCode.BadRequest => new BadRequestObjectResult(new
                {
                    statusCode = (int?)result.Error.HttpStatusCode.Value,
                    title = result.Error.HttpStatusCode.Value.ToString(),
                    type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                    extensions = new Dictionary<string, object?>
                    {
                        { "errors", new[] { result.Error } },
                        { "validationErrors", new[] { result.ValidationResults } }
                    }
                }
            ),
            _ => new ObjectResult(new
                {
                    statusCode = (int?)result.Error.HttpStatusCode.Value,
                    title = result.Error.HttpStatusCode.Value.ToString(),
                    type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5",
                    extensions = new Dictionary<string, object?>
                    {
                        { "errors", new[] { result.Error } },
                        { "validationErrors", new[] { result.ValidationResults } }
                    }
                }
            )
        };
    }
    
    public static ActionResult<T> ToProblemDetails<T>(this Result<T> result) where T : class
    {
        return result.Error.HttpStatusCode switch
        {
            HttpStatusCode.NotFound => new NotFoundObjectResult(new
                {
                    statusCode = (int?)result.Error.HttpStatusCode.Value,
                    title = result.Error.HttpStatusCode.Value.ToString(),
                    type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4",
                    extensions = new Dictionary<string, object?>
                    {
                        { "errors", new[] { result.Error } },
                        { "validationErrors", new[] { result.ValidationResults } }
                    }
                }
            ),
            HttpStatusCode.BadRequest => new BadRequestObjectResult(new
                {
                    statusCode = (int?)result.Error.HttpStatusCode.Value,
                    title = result.Error.HttpStatusCode.Value.ToString(),
                    type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                    extensions = new Dictionary<string, object?>
                    {
                        { "errors", new[] { result.Error } },
                        { "validationErrors", new[] { result.ValidationResults } }
                    }
                }
            ),
            _ => new ObjectResult(new
                {
                    statusCode = (int?)result.Error.HttpStatusCode.Value,
                    title = result.Error.HttpStatusCode.Value.ToString(),
                    type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5",
                    extensions = new Dictionary<string, object?>
                    {
                        { "errors", new[] { result.Error } },
                        { "validationErrors", new[] { result.ValidationResults } }
                    }
                }
            )
        };
    }
}