using WorkEnv.Domain.Enum;
using WorkEnv.Domain.ValueObjects;

namespace WorkEnv.Application.DTO.Activity;

public record ActivityDTO(
    Guid Id,
    Guid? AdminId,
    Guid WorkSpaceId,
    int NumberOfParticipants,
    int MaxNumberOfParticipants,
    Privacy Privacy, 
    ActivityStatus ActivityStatus,
    string? AccessPassword,
    Access AccessOptions,
    AdminInvite AdminInviteCode
);