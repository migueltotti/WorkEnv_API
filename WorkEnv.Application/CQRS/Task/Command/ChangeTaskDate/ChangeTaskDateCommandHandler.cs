using MediatR;
using WorkEnv.Application.Result;
using WorkEnv.Domain.Interfaces;

namespace WorkEnv.Application.CQRS.Task.Command.ChangeTaskDate;

public class ChangeTaskDateCommandHandler : IRequestHandler<ChangeTaskDateCommand, Result.Result>
{
    private readonly IUnitOfWork _uof;

    public ChangeTaskDateCommandHandler(IUnitOfWork uof)
    {
        _uof = uof;
    }

    public async Task<Result.Result> Handle(ChangeTaskDateCommand request, CancellationToken cancellationToken)
    {
        var task = await _uof.TaskRepository.GetByIdAsync(request.taskId, cancellationToken);

        if (task is null)
            return Result.Result.Failure(TaskErrors.ActivityNotFound);
        
        if(!task.AdminId.Equals(request.adminOrOwnerId) && 
           !task.WorkSpace.OwnerId.Equals(request.adminOrOwnerId))
            return Result.Result.Failure(ActivityErrors.AdminOrOwnerIdInvalid);
        
        var adminOrOwner = await _uof.UserRepository.GetByIdAsync(request.adminOrOwnerId, cancellationToken);

        if (adminOrOwner is null)
            return Result.Result.Failure(UserErrors.UserNotFound);
        
        task.ChangeDate(adminOrOwner.Id, request.newStartDate, request.newEndDate);
        
        _uof.TaskRepository.Update(task);
        await _uof.CommitChangesAsync(cancellationToken);
        
        return Result.Result.Success();
    }
}