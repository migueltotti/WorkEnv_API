using MediatR;

namespace WorkEnv.Application.CQRS.Auth.Register;

public record RegisterAuthUserCommand(
    string userId,
    string name,
    string email,
    string password
) : IRequest<Result.Result>;