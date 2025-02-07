using MediatR;
using WorkEnv.Application.Result;

namespace WorkEnv.Application.CQRS.WorkSpace.Query.GetMasterCode;

public record GetMasterCodeQuery(Guid workSpaceId, Guid ownerId) : IRequest<Result<string>>;