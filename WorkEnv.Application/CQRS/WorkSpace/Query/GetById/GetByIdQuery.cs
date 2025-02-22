using MediatR;
using WorkEnv.Application.DTO.WorkSpace;
using WorkEnv.Application.Result;

namespace WorkEnv.Application.CQRS.WorkSpace.Query.GetById;

public record GetByIdQuery(Guid workSpaceId) : IRequest<Result<WorkSpaceDTO>>;