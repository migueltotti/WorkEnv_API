using MediatR;

namespace WorkEnv.Application.CQRS.WorkSpace.Command.ChangeOwner;

public record ChangeOwnerCommand(Guid workSpaceId, Guid oldOwnerId, Guid newOwnerId) : IRequest<Result.Result>;