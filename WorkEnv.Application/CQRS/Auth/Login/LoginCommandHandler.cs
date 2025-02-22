using MediatR;
using Microsoft.Extensions.Configuration;
using WorkEnv.Application.DTO.Auth;
using WorkEnv.Application.Result;
using WorkEnv.Domain.Interfaces;

namespace WorkEnv.Application.CQRS.Auth.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<TokenResponse>>
{
    private readonly IUnitOfWork _uof;
    private readonly ITokenManager _tokenManager;
    private readonly IConfiguration _config;

    public LoginCommandHandler(IUnitOfWork uof, ITokenManager tokenManager, IConfiguration configuration)
    {
        _uof = uof;
        _tokenManager = tokenManager;
        _config = configuration;
    }

    public async Task<Result<TokenResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _uof.UserRepository.GetByEmailAsync(request.email, cancellationToken);
        
        if(user is null)
            return Result<TokenResponse>.Failure(UserErrors.UserNotFound);
        
        if(!user.Password.Equals(request.password))
            return Result<TokenResponse>.Failure(LoginError.PasswordIncorrect);

        var token = _tokenManager.GenerateAccessToken(user);
        var refreshToken = _tokenManager.GenerateRefreshToken();
        var refreshTokenExpiresAt = DateTime.UtcNow.AddMinutes(int.Parse(_config["Jwt:RefreshTokenValidityInMinutes"]));

        await _uof.UserRepository.SetRefreshToken(user.UserId, refreshToken, refreshTokenExpiresAt, cancellationToken);
        await _uof.CommitChangesAsync(cancellationToken);
        
        var tokenResponse = new TokenResponse(token, refreshToken, refreshTokenExpiresAt);
        
        return Result<TokenResponse>.Success(tokenResponse);
    }
}