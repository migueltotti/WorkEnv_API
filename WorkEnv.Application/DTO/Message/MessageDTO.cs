using WorkEnv.Domain.Enum;

namespace WorkEnv.Application.DTO.Message;

public record MessageDTO(
    Guid MessageId,
    Guid ActivityId,
    string? Title,
    string? Content,
    DateTime CreateDate
);
