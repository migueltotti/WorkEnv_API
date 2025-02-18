using MediatR;
using WorkEnv.Application.DTO.Auth;
using WorkEnv.Application.Result;

namespace WorkEnv.Application.CQRS.Auth.Login;

public record LoginCommand(string email, string password) : IRequest<Result<TokenResponse>>;