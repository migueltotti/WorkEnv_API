using MediatR;
using WorkEnv.Application.Result;
using WorkEnv.Domain.Interfaces;

namespace WorkEnv.Application.CQRS.Activity.Command.Delete;

public class DeleteCommandHandler : IRequestHandler<DeleteCommand, Result.Result>
{
    private readonly IUnitOfWork _uof;

    public DeleteCommandHandler(IUnitOfWork uof)
    {
        _uof = uof;
    }

    public async Task<Result.Result> Handle(DeleteCommand request, CancellationToken cancellationToken)
    {
        var activity = await _uof.ActivityRepository.GetByIdAsync(request.activityId, cancellationToken);

        if (activity is null)
            return Result.Result.Failure(ActivityErrors.ActivityNotFound);
        
        if(!activity.WorkSpace.OwnerId.Equals(request.ownerId))
            return Result.Result.Failure(WorkSpaceErrors.OwnerIdMismatch);
        
        var owner = await _uof.UserRepository.GetByIdAsync(request.ownerId, cancellationToken);

        if (owner is null)
            return Result.Result.Failure(UserErrors.UserNotFound);
        
        if(!activity.WorkSpace.GetMasterCode(owner.Id)!.Equals(request.masterCode))
            return Result.Result.Failure(WorkSpaceErrors.IncorrectMasterCode);
        
        _uof.ActivityRepository.Delete(activity);
        await _uof.CommitChangesAsync(cancellationToken);
        
        return Result.Result.Success();
    }
}