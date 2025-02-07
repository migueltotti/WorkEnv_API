using MediatR;
using WorkEnv.Application.DTO.WorkSpace;

namespace WorkEnv.Application.CQRS.WorkSpace.Query.GetAll;

public record GetAllQuery() : IRequest<List<WorkSpaceDTO>>;