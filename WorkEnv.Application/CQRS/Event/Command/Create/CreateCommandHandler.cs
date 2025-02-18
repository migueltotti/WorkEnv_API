using MediatR;
using WorkEnv.Application.DTO.Task;
using WorkEnv.Application.Map;
using WorkEnv.Application.Result;
using WorkEnv.Domain.Interfaces;

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
        var workSpace = await _uof.WorkSpaceRepository.GetByIdAsync(request.workSpaceId, cancellationToken);
        
        if(workSpace is null)
            return Result<EventDTO>.Failure(WorkSpaceErrors.WorkSpaceNotFound);

        if (request.adminId is not null)
        {
            var admin = await _uof.UserRepository.GetByIdAsync(request.adminId.Value, cancellationToken);
        
            if(admin is null)
                return Result<EventDTO>.Failure(UserErrors.UserNotFound);
        }
        
        var @event = new Domain.Entities.Event(
            Guid.NewGuid(),
            request.workSpaceId,
            request.maxNumberOfParticipants,
            request.privacy,
            request.activityStatus,
            request.accessOptions,
            request.eventDate,
            request.adminId
        );
        
        await _uof.EventRepository.AddAsync(@event, cancellationToken);
        await _uof.CommitChangesAsync(cancellationToken);
        
        return Result<EventDTO>.Success(@event.ToEventDto());
    }
}