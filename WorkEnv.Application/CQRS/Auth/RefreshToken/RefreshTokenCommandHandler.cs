using System.Security.Claims;
using MediatR;
using Microsoft.Extensions.Configuration;
using WorkEnv.Application.CQRS.Auth.Login;
using WorkEnv.Application.DTO.Auth;
using WorkEnv.Application.Result;
using WorkEnv.Domain.Interfaces;

namespace WorkEnv.Application.CQRS.Auth.RefreshToken;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Result<TokenResponse>>
{
    private readonly IUnitOfWork _uof;
    private readonly ITokenManager _tokenManager;
    private readonly IConfiguration _config;

    public RefreshTokenCommandHandler(IUnitOfWork uof, ITokenManager tokenManager, IConfiguration configuration)
    {
        _uof = uof;
        _tokenManager = tokenManager;
        _config = configuration;
    }
    
    public async Task<Result<TokenResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var claims = _tokenManager.GetClaimsFromExpiredToken(request.accessToken);

        var email = claims.Claims
            .FirstOrDefault(c => c.Type.Equals(ClaimTypes.Email))!
            .Value;
        
        var user = await _uof.UserRepository.GetByEmailAsync(email, cancellationToken);

        if (user is null)
            return Result<TokenResponse>.Failure(UserErrors.UserNotFound);

        var isRefreshTokenValid = await _uof.UserRepository
            .ValidateRefreshToken(user.Id, request.refreshToken, cancellationToken);
        
        if(!isRefreshTokenValid)
            return Result<TokenResponse>.Failure(LoginError.RefreshTokenInvalid);

        var token = _tokenManager.GenerateAccessToken(user);
        var refreshToken = _tokenManager.GenerateRefreshToken();
        var refreshTokenExpiresAt = DateTime.UtcNow.AddMinutes(int.Parse(_config["Jwt:RefreshTokenValidityInMinutes"]));

        await _uof.UserRepository.SetRefreshToken(user.Id, refreshToken, refreshTokenExpiresAt,  cancellationToken);
        await _uof.CommitChangesAsync(cancellationToken);
        
        var tokenResponse = new TokenResponse(token, refreshToken, refreshTokenExpiresAt);
        
        return Result<TokenResponse>.Success(tokenResponse);
    }
}