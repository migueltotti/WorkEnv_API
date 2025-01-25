using WorkEnv.Domain.Entities;
using WorkEnv.Domain.Enum;

namespace WorkEnv.Domain.ValueObjects;

public record Message(
    Guid MessageId,
    Guid ActivityId,
    Activity Activity,
    string Content,
    DateTime CreateDate,
    MessageType MessageType
);