namespace WorkEnv.Domain.ValueObjects;

public record AdminInvite(
    int Code,
    DateTime CodeExpirationDate
);