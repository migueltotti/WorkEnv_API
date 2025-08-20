using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using WorkEnv.Application.DTO.Auth;
using WorkEnv.Application.Result;
using WorkEnv.Domain.Entities;
using WorkEnv.Domain.Interfaces;
using WorkEnv.Infrastructure.Authentication;
using WorkEnv.Infrastructure.Identity;

namespace WorkEnv.Application.CQRS.Auth.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<TokenResponse>>
{
    private readonly IUnitOfWork _uof;
    private readonly ITokenManager _tokenManager;
    private readonly IConfiguration _config;
    private readonly IValidator<LoginCommand> _loginValidator;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IEncryptService _encryptService;

    public LoginCommandHandler(IUnitOfWork uof, ITokenManager tokenManager, IConfiguration configuration, IValidator<LoginCommand> loginValidator, UserManager<ApplicationUser> userManager, IEncryptService encryptService)
    {
        _uof = uof;
        _tokenManager = tokenManager;
        _config = configuration;
        _loginValidator = loginValidator;
        _userManager = userManager;
        _encryptService = encryptService;
    }

    public async Task<Result<TokenResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var result = await _loginValidator.ValidateAsync(request, cancellationToken);
        
        if(result is null)
            return Result<TokenResponse>.Failure(LoginErrors.IncorrectFormat);

        var user = await _userManager.FindByEmailAsync(request.email);
        
        if(user is null)
            return Result<TokenResponse>.Failure(UserErrors.UserNotFound);

        var decryptedPassword = _encryptService.Decrypt(request.password);
        
        var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, decryptedPassword);
        
        if(!isPasswordCorrect)
            return Result<TokenResponse>.Failure(LoginErrors.IncorrectPassword);

        var token = _tokenManager.GenerateAccessToken(user);
        var refreshToken = _tokenManager.GenerateRefreshToken();
        var refreshTokenExpiresAt = DateTime.UtcNow
            .AddMinutes(int.Parse(_config["Jwt:RefreshTokenValidityInMinutes"]));
        
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpireAt = refreshTokenExpiresAt;
        
        await _userManager.UpdateAsync(user);
        
        var tokenResponse = new TokenResponse(token, refreshToken, refreshTokenExpiresAt);
        
        return Result<TokenResponse>.Success(tokenResponse);
    }
}