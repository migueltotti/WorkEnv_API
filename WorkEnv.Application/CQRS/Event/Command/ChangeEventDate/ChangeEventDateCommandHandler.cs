using MediatR;
using WorkEnv.Application.Result;
using WorkEnv.Domain.Interfaces;

namespace WorkEnv.Application.CQRS.Event.Command.ChangeEventDate;

public class ChangeEventDateCommandHandler : IRequestHandler<ChangeEventDateCommand, Result.Result>
{
    private readonly IUnitOfWork _uof;

    public ChangeEventDateCommandHandler(IUnitOfWork uof)
    {
        _uof = uof;
    }
    
    public async Task<Result.Result> Handle(ChangeEventDateCommand request, CancellationToken cancellationToken)
    {
        /*var @event = await _uof.EventRepository.GetByIdAsync(request.eventId, cancellationToken);

        if (@event is null)
            return Result.Result.Failure(EventErrors.ActivityNotFound);
        
        if(!@event.AdminId.Equals(request.adminOrOwnerId) && 
           !@event.WorkSpace.OwnerId.Equals(request.adminOrOwnerId))
            return Result.Result.Failure(ActivityErrors.AdminOrOwnerIdInvalid);
        
        var adminOrOwner = await _uof.UserRepository.GetByIdAsync(request.adminOrOwnerId, cancellationToken);

        if (adminOrOwner is null)
            return Result.Result.Failure(UserErrors.UserNotFound);
        
        @event.ChangeEventDate(adminOrOwner.Id, request.newEventDate);
        
        _uof.EventRepository.Update(@event);
        await _uof.CommitChangesAsync(cancellationToken);*/
        
        return Result.Result.Success();
    }
}