using MediatR;
using WorkEnv.Application.DTO.Task;
using WorkEnv.Application.Result;

namespace WorkEnv.Application.CQRS.Task.Query.GetById;

public record GetByIdQuery(Guid taskId) : IRequest<Result<TaskDTO>>;