using MediatR;
using WorkEnv.Application.Result;

namespace WorkEnv.Application.CQRS.WorkSpace.Command.Delete;

public record DeleteCommand(Guid workSpaceId, Guid ownerId) : IRequest<Result.Result>;