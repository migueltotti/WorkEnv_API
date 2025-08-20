using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using WorkEnv.Application.CQRS.Auth.Login;
using WorkEnv.Application.DTO.Auth;
using WorkEnv.Application.Result;
using WorkEnv.Domain.Entities;
using WorkEnv.Domain.Interfaces;
using WorkEnv.Infrastructure.Authentication;
using WorkEnv.Infrastructure.Identity;

namespace WorkEnv.Application.CQRS.Auth.RefreshToken;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Result<TokenResponse>>
{
    private readonly IUnitOfWork _uof;
    private readonly ITokenManager _tokenManager;
    private readonly IConfiguration _config;
    private readonly UserManager<ApplicationUser> _userManager;

    public RefreshTokenCommandHandler(IUnitOfWork uof, ITokenManager tokenManager, IConfiguration configuration, UserManager<ApplicationUser> userManager)
    {
        _uof = uof;
        _tokenManager = tokenManager;
        _config = configuration;
        _userManager = userManager;
    }
    
    public async Task<Result<TokenResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var claims = _tokenManager.GetClaimsFromExpiredToken(request.accessToken);

        if (claims is null)
            return Result<TokenResponse>.Failure(RefreshTokenErrors.IncorrectTokens);
        
        var email = claims.Claims
            .FirstOrDefault(c => c.Type.Equals(ClaimTypes.Email))!
            .Value;
        
        var user = await _userManager.FindByEmailAsync(email);

        if (user is null || user.RefreshToken != request.refreshToken || user.RefreshTokenExpireAt <= DateTime.Now)
            return Result<TokenResponse>.Failure(UserErrors.UserNotFound);
        
        var newAccessToken = _tokenManager.GenerateAccessToken(user);
        var newRefreshToken = _tokenManager.GenerateRefreshToken();
        var newRefreshTokenExpiration = DateTime.UtcNow.AddMinutes(int.Parse(_config["Jwt:RefreshTokenValidityInMinutes"]));

        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpireAt = newRefreshTokenExpiration;
        
        await _userManager.UpdateAsync(user);
        
        var tokenResponse = new TokenResponse(newAccessToken, newRefreshToken, newRefreshTokenExpiration);
        
        return Result<TokenResponse>.Success(tokenResponse);
    }
}