using MediatR;
using WorkEnv.Application.DTO.Activity;
using WorkEnv.Application.Result;

namespace WorkEnv.Application.CQRS.WorkSpace.Query.GetAllActivitiesByWorkSpaceId;

public record GetAllActivitiesByWorkSpaceIdQuery(Guid workSpaceId, Guid ownerId) : IRequest<Result<List<ActivityDTO>>>;