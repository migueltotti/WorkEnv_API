using MediatR;
using WorkEnv.Application.DTO.Task;
using WorkEnv.Application.Result;

namespace WorkEnv.Application.CQRS.Event.Query.GetById;

public record GetByIdQuery(Guid eventId) : IRequest<Result<EventDTO>>;