using MediatR;
using WorkEnv.Application.Result;
using WorkEnv.Domain.Interfaces;

namespace WorkEnv.Application.CQRS.Activity.Command.SendUserInvite;

public class SendUserInviteCommandHandler : IRequestHandler<SendUserInviteCommand, Result.Result>
{
    private readonly IUnitOfWork _uof;

    public SendUserInviteCommandHandler(IUnitOfWork uof)
    {
        _uof = uof;
    }

    public async Task<Result.Result> Handle(SendUserInviteCommand request, CancellationToken cancellationToken)
    {
        /*var activity = await _uof.ActivityRepository.GetByIdAsync(request.activityId, cancellationToken);

        if (activity is null)
            return Result.Result.Failure(ActivityErrors.ActivityNotFound);

        if (!activity.AdminId.Equals(request.adminOrOwnerId) && 
            !activity.WorkSpace.OwnerId.Equals(request.adminOrOwnerId))
            return Result.Result.Failure(ActivityErrors.AdminOrOwnerIdInvalid);

        var adminOrOwner = await _uof.UserRepository.GetByIdAsync(request.adminOrOwnerId, cancellationToken);
        
        if(adminOrOwner is null)
            return Result.Result.Failure(UserErrors.UserNotFound);
        
        var user = await _uof.UserRepository.GetByIdAsync(request.userId, cancellationToken);
        
        if(user is null)
            return Result.Result.Failure(UserErrors.UserNotFound);
        
        // TODO: Send user invite by email using RabbitMQ and FluentEmail
        // There will be a link in the email that leads to a login page to user authenticate themselve
        // Then, this page will have a button to confirm the accept invite
        // This button will access the AcceptUserInviteCommandHandler and do the rest of the job.
        
        _uof.ActivityRepository.Update(activity);
        await _uof.CommitChangesAsync(cancellationToken);*/
        
        return Result.Result.Success();
    }
}