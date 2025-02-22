using MediatR;

namespace WorkEnv.Application.CQRS.User.Command.Delete;

public record DeleteUserCommand(Guid userId) : IRequest<Result.Result>;