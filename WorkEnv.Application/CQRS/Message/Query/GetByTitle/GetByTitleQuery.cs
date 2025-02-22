using MediatR;
using WorkEnv.Application.DTO.Message;
using WorkEnv.Application.Result;

namespace WorkEnv.Application.CQRS.Message.Query.GetByTitle;

public record GetByTitleQuery(string messageTitle) : IRequest<Result<MessageDTO>>;