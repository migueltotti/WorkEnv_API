using MediatR;
using WorkEnv.Application.DTO.Message;
using WorkEnv.Application.Result;

namespace WorkEnv.Application.CQRS.Message.Query.GetById;

public record GetByIdQuery(Guid messageId) : IRequest<Result<MessageDTO>>;