using MediatR;
using WorkEnv.Application.Result;
using WorkEnv.Domain.Interfaces;

namespace WorkEnv.Application.CQRS.Message.Command.Delete;

public class DeleteCommandHandler: IRequestHandler<DeleteCommand, Result.Result>
{
    private readonly IUnitOfWork _uof;

    public DeleteCommandHandler(IUnitOfWork uof)
    {
        _uof = uof;
    }

    public async Task<Result.Result> Handle(DeleteCommand request, CancellationToken cancellationToken)
    {
        var message = await _uof.MessageRepository.GetByIdAsync(request.messageId, cancellationToken);
        
        if(message is null)
            return Result.Result.Failure(MessageErrors.MessageNotFound);
        
        var activity = await _uof.ActivityRepository.GetByIdAsync(request.activityId, cancellationToken);
        
        if(activity is null)
            return Result.Result.Failure(ActivityErrors.ActivityNotFound);
        
        if (!activity.AdminId.Equals(request.adminOrOwnerId) && 
            !activity.WorkSpace.OwnerId.Equals(request.adminOrOwnerId))
            return Result.Result.Failure(ActivityErrors.AdminOrOwnerIdInvalid);

        var adminOrOwner = await _uof.UserRepository.GetByIdAsync(request.adminOrOwnerId, cancellationToken);
        
        if(adminOrOwner is null)
            return Result.Result.Failure(UserErrors.UserNotFound);
        
        _uof.ActivityRepository.Delete(activity);
        await _uof.CommitChangesAsync(cancellationToken);
        
        return Result.Result.Success();
    }
}