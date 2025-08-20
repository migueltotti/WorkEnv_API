using MediatR;

namespace WorkEnv.Application.CQRS.Auth.Register;

public record RegisterAuthUserCommand(
    string name,
    string email,
    string password
) : IRequest<Result.Result>;