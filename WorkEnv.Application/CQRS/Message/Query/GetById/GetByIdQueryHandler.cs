using MediatR;
using WorkEnv.Application.DTO.Map;
using WorkEnv.Application.DTO.Message;
using WorkEnv.Application.Result;
using WorkEnv.Domain.Interfaces;

namespace WorkEnv.Application.CQRS.Message.Query.GetById;

public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, Result<MessageDTO>>
{
    private readonly IUnitOfWork _uof;

    public GetByIdQueryHandler(IUnitOfWork uof)
    {
        _uof = uof;
    }

    public async Task<Result<MessageDTO>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var message = await _uof.MessageRepository.GetByIdAsync(request.messageId, cancellationToken);

        if (message is null)
            return Result<MessageDTO>.Failure(MessageErrors.MessageNotFound);
        
        return Result<MessageDTO>.Success(message.ToMessageDto());
    }
}