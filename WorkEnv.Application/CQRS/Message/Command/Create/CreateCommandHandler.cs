using MediatR;
using WorkEnv.Application.DTO.Map;
using WorkEnv.Application.DTO.Message;
using WorkEnv.Application.Result;
using WorkEnv.Domain.Interfaces;

namespace WorkEnv.Application.CQRS.Message.Command.Create;

public class CreateCommandHandler : IRequestHandler<CreateCommand, Result<MessageDTO>>
{
    private readonly IUnitOfWork _uof;

    public CreateCommandHandler(IUnitOfWork uof)
    {
        _uof = uof;
    }

    public async Task<Result<MessageDTO>> Handle(CreateCommand request, CancellationToken cancellationToken)
    {
        var activity = await _uof.ActivityRepository.GetByIdAsync(request.activityId, cancellationToken);
        
        if(activity is null)
            return Result<MessageDTO>.Failure(ActivityErrors.ActivityNotFound);

        if(!activity.AdminId.Equals(request.ownerOrAdminId) &&
           !activity.WorkSpace.OwnerId.Equals(request.ownerOrAdminId))
            return Result<MessageDTO>.Failure(ActivityErrors.AdminOrOwnerIdInvalid);
        
        var message = new Domain.Entities.Message(
            Guid.NewGuid(),
            request.activityId,
            request.title,
            request.content,
            request.messageType
        );
        
        await _uof.MessageRepository.AddAsync(message, cancellationToken);
        await _uof.CommitChangesAsync(cancellationToken);
        
        return Result<MessageDTO>.Success(message.ToMessageDto());
    }
}