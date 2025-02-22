using MediatR;
using WorkEnv.Application.DTO.Map;
using WorkEnv.Application.DTO.Message;
using WorkEnv.Application.Result;
using WorkEnv.Domain.Interfaces;

namespace WorkEnv.Application.CQRS.Message.Query.GetByTitle;

public class GetByTitleQueryHandler : IRequestHandler<GetByTitleQuery, Result<MessageDTO>>
{
    private readonly IUnitOfWork _uof;

    public GetByTitleQueryHandler(IUnitOfWork uof)
    {
        _uof = uof;
    }

    public async Task<Result<MessageDTO>> Handle(GetByTitleQuery request, CancellationToken cancellationToken)
    {
        var message = await _uof.MessageRepository.GetByTitleAsync(request.messageTitle, cancellationToken);

        if (message is null)
            return Result<MessageDTO>.Failure(MessageErrors.MessageNotFound);
        
        return Result<MessageDTO>.Success(message.ToMessageDto());
    }
}