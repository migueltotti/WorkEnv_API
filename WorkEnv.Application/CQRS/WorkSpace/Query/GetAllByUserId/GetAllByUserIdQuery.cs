using MediatR;
using WorkEnv.Application.DTO.WorkSpace;
using WorkEnv.Application.Result;

namespace WorkEnv.Application.CQRS.WorkSpace.Query.GetAllByUserId;

public record GetAllByUserIdQuery(Guid userId) : IRequest<Result<List<WorkSpaceDTO>>>;