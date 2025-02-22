using MediatR;
using WorkEnv.Application.DTO.Map;
using WorkEnv.Application.DTO.Task;
using WorkEnv.Application.Result;
using WorkEnv.Domain.Interfaces;

namespace WorkEnv.Application.CQRS.Task.Query.GetById;

public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, Result<TaskDTO>>
{
    private readonly IUnitOfWork _uof;

    public GetByIdQueryHandler(IUnitOfWork uof)
    {
        _uof = uof;
    }

    public async Task<Result<TaskDTO>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var task = await _uof.TaskRepository.GetByIdAsync(request.taskId, cancellationToken);

        if (task is null)
            return Result<TaskDTO>.Failure(ActivityErrors.ActivityNotFound);

        return Result<TaskDTO>.Success(task.ToTaskDto());
    }
}