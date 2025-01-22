using WorkEnv.Domain.Enum;

namespace WorkEnv.Domain.ValueObjects;

public record Message(
    Guid MessageId,
    Guid ActivityId,
    string Content,
    DateTime CreateDate,
    MessageType MessageType
);