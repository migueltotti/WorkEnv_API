using MediatR;
using WorkEnv.Application.Result;
using WorkEnv.Domain.Interfaces;

namespace WorkEnv.Application.CQRS.Activity.Command.ChangeAccessPassword;

public class ChangeAccessPasswordCommandHandler : IRequestHandler<ChangeAccessPasswordCommand, Result.Result>
{
    private readonly IUnitOfWork _uof;

    public ChangeAccessPasswordCommandHandler(IUnitOfWork uof)
    {
        _uof = uof;
    }   
    
    public async Task<Result.Result> Handle(ChangeAccessPasswordCommand request, CancellationToken cancellationToken)
    {
        var activity = await _uof.ActivityRepository.GetByIdAsync(request.activityId, cancellationToken);

        if (activity is null)
            return Result.Result.Failure(ActivityErrors.ActivityNotFound);

        if (!activity.AdminId.Equals(request.adminOrOwnerId) && 
            !activity.WorkSpace.OwnerId.Equals(request.adminOrOwnerId))
            return Result.Result.Failure(ActivityErrors.AdminOrOwnerIdInvalid);

        var adminOrOwner = await _uof.UserRepository.GetByIdAsync(request.adminOrOwnerId, cancellationToken);
        
        if(adminOrOwner is null)
            return Result.Result.Failure(UserErrors.UserNotFound);
        
        activity.ChangeAccessPassword(adminOrOwner.UserId);
        
        _uof.ActivityRepository.Update(activity);
        await _uof.CommitChangesAsync(cancellationToken);
        
        return Result.Result.Success();
    }
}