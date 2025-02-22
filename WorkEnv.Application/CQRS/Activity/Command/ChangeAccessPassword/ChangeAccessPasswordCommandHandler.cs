using MediatR;
using WorkEnv.Application.Result;
using WorkEnv.Domain.Interfaces;

namespace WorkEnv.Application.CQRS.Activity.Command.ChangeAccessPassword;

public class ChangeAccessPasswordCommandHandler : IRequestHandler<ChangeAccessPasswordCommand, Result<object>>
{
    private readonly IUnitOfWork _uof;

    public ChangeAccessPasswordCommandHandler(IUnitOfWork uof)
    {
        _uof = uof;
    }   
    
    public async Task<Result<object>> Handle(ChangeAccessPasswordCommand request, CancellationToken cancellationToken)
    {
        var activity = await _uof.ActivityRepository.GetByIdAsync(request.activityId, cancellationToken);

        if (activity is null)
            return Result<object>.Failure(ActivityErrors.ActivityNotFound);

        if (!activity.AdminId.Equals(request.adminOrOwnerId) && 
            !activity.WorkSpace.OwnerId.Equals(request.adminOrOwnerId))
            return Result<object>.Failure(ActivityErrors.AdminOrOwnerIdInvalid);

        var adminOrOwner = await _uof.UserRepository.GetByIdAsync(request.adminOrOwnerId, cancellationToken);
        
        if(adminOrOwner is null)
            return Result<object>.Failure(UserErrors.UserNotFound);
        
        activity.ChangeAccessPassword(adminOrOwner.UserId);
        
        _uof.ActivityRepository.Update(activity);
        await _uof.CommitChangesAsync(cancellationToken);
        
        return Result<object>.Success(activity.AccessPassword);
    }
}