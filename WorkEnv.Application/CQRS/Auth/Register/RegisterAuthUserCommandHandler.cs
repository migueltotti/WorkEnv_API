using System.Net;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Logging;
using WorkEnv.Application.Result;
using WorkEnv.Domain.Entities;
using WorkEnv.Infrastructure.Identity;

namespace WorkEnv.Application.CQRS.Auth.Register;

public class RegisterAuthUserCommandHandler : IRequestHandler<RegisterAuthUserCommand, Result.Result>
{
    private readonly IValidator<RegisterAuthUserCommand> _registerValidator;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<RegisterAuthUserCommandHandler> _logger;

    public RegisterAuthUserCommandHandler(IValidator<RegisterAuthUserCommand> registerValidator, UserManager<ApplicationUser> userManager, ILogger<RegisterAuthUserCommandHandler> logger)
    {
        _registerValidator = registerValidator;
        _userManager = userManager;
        _logger = logger;
    }

    public async Task<Result.Result> Handle(RegisterAuthUserCommand request, CancellationToken cancellationToken)
    {
        var result = await _registerValidator.ValidateAsync(request, cancellationToken);

        if (!result.IsValid)
            return Result.Result.Failure(RegisterAuthUserErrors.IncorrectFormat);
        
        var userExists = await _userManager.FindByEmailAsync(request.email);

        if (userExists is not null)
            return Result.Result.Failure(RegisterAuthUserErrors.AuthUserExists);

        var newApplicationUser = new ApplicationUser()
        {
            Id = request.userId,
            Email = request.email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = request.name.Replace(" ", "") + "@" + Guid.NewGuid(),
            //LockoutEnd = DateTime.SpecifyKind(DateTime.MinValue, DateTimeKind.Utc)
        };

        var resultUser = await _userManager.CreateAsync(newApplicationUser, request.password);

        if (!resultUser.Succeeded)
            return Result.Result.Failure(RegisterAuthUserErrors.CreationFailed);
        
        return Result.Result.Success();
    }
}