using MediatR;
using WorkEnv.Application.DTO.Map;
using WorkEnv.Application.DTO.Task;
using WorkEnv.Application.Map;
using WorkEnv.Application.Result;
using WorkEnv.Domain.Interfaces;

namespace WorkEnv.Application.CQRS.Event.Query.GetById;

public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, Result<EventDTO>>
{
    private readonly IUnitOfWork _uof;

    public GetByIdQueryHandler(IUnitOfWork uof)
    {
        _uof = uof;
    }

    public async Task<Result<EventDTO>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var @event = await _uof.EventRepository.GetByIdAsync(request.eventId, cancellationToken);

        if (@event is null)
            return Result<EventDTO>.Failure(ActivityErrors.ActivityNotFound);

        return Result<EventDTO>.Success(@event.ToEventDto());
    }
}