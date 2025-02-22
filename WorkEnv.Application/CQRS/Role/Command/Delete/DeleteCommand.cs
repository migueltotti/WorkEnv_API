using MediatR;

namespace WorkEnv.Application.CQRS.Role.Command.Delete;

public record DeleteCommand(Guid roleId) : IRequest<Result.Result>;