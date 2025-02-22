using MediatR;
using WorkEnv.Application.DTO.Auth;
using WorkEnv.Application.Result;

namespace WorkEnv.Application.CQRS.Auth.RefreshToken;

public record RefreshTokenCommand(string accessToken, string refreshToken) : IRequest<Result<TokenResponse>>;