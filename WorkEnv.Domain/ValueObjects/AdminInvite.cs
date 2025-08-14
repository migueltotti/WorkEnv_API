namespace WorkEnv.Domain.ValueObjects;

public record AdminInvite(
    string Code,
    DateTime ExpirationDate
);