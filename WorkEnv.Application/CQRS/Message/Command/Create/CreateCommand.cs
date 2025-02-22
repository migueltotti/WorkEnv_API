using MediatR;
using WorkEnv.Application.DTO.Message;
using WorkEnv.Domain.Enum;
using WorkEnv.Application.Result;

namespace WorkEnv.Application.CQRS.Message.Command.Create;

public record CreateCommand(
    Guid activityId,
    Guid ownerOrAdminId,
    string? title,
    string? content,
    MessageType messageType
) : IRequest<Result<MessageDTO>>;