using MediatR;
using WorkEnv.Application.DTO.Task;
using WorkEnv.Application.Map;
using WorkEnv.Application.Result;
using WorkEnv.Domain.Enum;
using WorkEnv.Domain.Interfaces;
using WorkEnv.Domain.ValueObjects;
using TaskStatus = WorkEnv.Domain.Enum.TaskStatus;

namespace WorkEnv.Application.CQRS.Event.Command.Create;

public class CreateCommandHandler : IRequestHandler<CreateCommand, Result<EventDTO>>
{
    private readonly IUnitOfWork _uof;

    public CreateCommandHandler(IUnitOfWork uof)
    {
        _uof = uof;
    }

    public async Task<Result<EventDTO>> Handle(CreateCommand request, CancellationToken cancellationToken)
    {
        /*var workSpace = await _uof.WorkSpaceRepository.GetByIdAsync(request.workSpaceId, cancellationToken);
        
        if(workSpace is null)
            return Result<EventDTO>.Failure(WorkSpaceErrors.WorkSpaceNotFound);

        if(!workSpace.OwnerId.Equals(request.ownerId))
            return Result<EventDTO>.Failure(WorkSpaceErrors.OwnerIdMismatch);
        
        if (request.adminId is not null)
        {
            var admin = await _uof.UserRepository.GetByIdAsync(request.adminId.Value, cancellationToken);
        
            if(admin is null)
                return Result<EventDTO>.Failure(UserErrors.UserNotFound);
        }
       
        var @event = new Domain.Entities.Event(
            Guid.NewGuid(),
            request.workSpaceId,
            request.name,
            request.maxNumberOfParticipants,
            request.privacy,
            request.TaskStatus,
            request.EventAccessOptionOptions,
            request.eventDate,
            request.adminId
        );
        
        await _uof.EventRepository.AddAsync(@event, cancellationToken);
        await _uof.CommitChangesAsync(cancellationToken);
         */
        var eventDto = new EventDTO(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            0,
            0, 
            Privacy.Private, 
            TaskStatus.Canceled, 
            null, 
            EventAccessOption.PasswordRequired, 
            new AdminInvite("", DateTime.Now, ""),
            DateTime.Now
        );
        
        return Result<EventDTO>.Success(eventDto);
    }
}