using MediatR;
using WorkEnv.Application.Result;
using WorkEnv.Domain.Interfaces;

namespace WorkEnv.Application.CQRS.Activity.Command.SendAdminInvite;

public class SendAdminInviteCommandHandler : IRequestHandler<SendAdminInviteCommand, Result.Result>
{
    private readonly IUnitOfWork _uof;

    public SendAdminInviteCommandHandler(IUnitOfWork uof)
    {
        _uof = uof;
    }

    public async Task<Result.Result> Handle(SendAdminInviteCommand request, CancellationToken cancellationToken)
    {
        var activity = await _uof.ActivityRepository.GetByIdAsync(request.activityId, cancellationToken);
        
        if(activity is null)
            return Result.Result.Failure(ActivityErrors.ActivityNotFound);
        
        if(!activity.WorkSpace.OwnerId.Equals(request.ownerId))
            return Result.Result.Failure(WorkSpaceErrors.OwnerIdMismatch);
        
        var owner = await _uof.UserRepository.GetByIdAsync(request.ownerId, cancellationToken);
        
        if(owner is null)
            return Result.Result.Failure(UserErrors.UserNotFound);
        
        var admin = await _uof.UserRepository.GetByIdAsync(request.newAdminId, cancellationToken);
        
        if(admin is null)
            return Result.Result.Failure(UserErrors.UserNotFound);

        var inviteCode = activity.GenerateAdminInviteCode(owner.Id);
        
        // Send and Email to new Admin with the inviteCode
        // RabbitMQ

        return Result.Result.Success();
    }
}