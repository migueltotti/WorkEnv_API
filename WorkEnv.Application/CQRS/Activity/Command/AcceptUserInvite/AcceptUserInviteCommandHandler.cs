using MediatR;
using WorkEnv.Application.CQRS.Activity.Command.SendUserInvite;
using WorkEnv.Application.Result;
using WorkEnv.Domain.Entities;
using WorkEnv.Domain.Enum;
using WorkEnv.Domain.Interfaces;

namespace WorkEnv.Application.CQRS.Activity.Command.AcceptUserInvite;

public class AcceptUserInviteCommandHandler : IRequestHandler<AcceptUserInviteCommand, Result.Result>
{
    private readonly IUnitOfWork _uof;

    public AcceptUserInviteCommandHandler(IUnitOfWork uof)
    {
        _uof = uof;
    }

    public async Task<Result.Result> Handle(AcceptUserInviteCommand request, CancellationToken cancellationToken)
    {
        var activity = await _uof.ActivityRepository.GetByIdAsync(request.activityId, cancellationToken);

        if (activity is null)
            return Result.Result.Failure(ActivityErrors.ActivityNotFound);
        
        var user = await _uof.UserRepository.GetByIdAsync(request.userId, cancellationToken);
        
        if(user is null)
            return Result.Result.Failure(UserErrors.UserNotFound);

        if(activity.IsActivityFullOfUsers())
            return Result.Result.Failure(ActivityErrors.IncorrectAccessPassword);
        
        if (activity.AccessOptions.Equals(EventAccessOption.PasswordRequired))
        {
            if (String.IsNullOrEmpty(request.password))
                return Result.Result.Failure(ActivityErrors.NullOrEmptyPassword);
            
            if(!activity.AccessPassword.Equals(request.password))
                return Result.Result.Failure(ActivityErrors.IncorrectAccessPassword);
        }
            
        var userActivity = new UserActivity(user.Id, activity.Id);
            
        activity.AddUser(userActivity);
        
        _uof.ActivityRepository.Update(activity);
        await _uof.CommitChangesAsync(cancellationToken);
        
        return Result.Result.Success();
    }
}