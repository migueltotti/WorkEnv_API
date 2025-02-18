using MediatR;
using WorkEnv.Application.DTO.Map;
using WorkEnv.Application.DTO.Task;
using WorkEnv.Application.Result;
using WorkEnv.Domain.Interfaces;

namespace WorkEnv.Application.CQRS.Task.Command.Create;

public class CreateCommandHandler : IRequestHandler<CreateCommand, Result<TaskDTO>>
{
    private readonly IUnitOfWork _uof;

    public CreateCommandHandler(IUnitOfWork uof)
    {
        _uof = uof;
    }

    public async Task<Result<TaskDTO>> Handle(CreateCommand request, CancellationToken cancellationToken)
    {
        var workSpace = await _uof.WorkSpaceRepository.GetByIdAsync(request.workSpaceId, cancellationToken);
        
        if(workSpace is null)
            return Result<TaskDTO>.Failure(WorkSpaceErrors.WorkSpaceNotFound);

        if (request.adminId is not null)
        {
            var admin = await _uof.UserRepository.GetByIdAsync(request.adminId.Value, cancellationToken);
        
            if(admin is null)
                return Result<TaskDTO>.Failure(UserErrors.UserNotFound);
        }

        var task = new Domain.Entities.Task(
            Guid.NewGuid(),
            request.workSpaceId,
            request.maxNumberOfParticipants,
            request.privacy,
            request.activityStatus,
            request.accessOptions,
            request.startDate,
            request.endDate,
            request.adminId
        );
        
        await _uof.TaskRepository.AddAsync(task, cancellationToken);
        await _uof.CommitChangesAsync(cancellationToken);
        
        return Result<TaskDTO>.Success(task.ToTaskDto());
    }
}