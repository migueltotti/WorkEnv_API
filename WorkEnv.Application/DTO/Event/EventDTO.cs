using WorkEnv.Application.DTO.Activity;
using WorkEnv.Domain.Enum;
using WorkEnv.Domain.ValueObjects;

namespace WorkEnv.Application.DTO.Task;

public record EventDTO(
    Guid Id,
    Guid? AdminId,
    Guid WorkSpaceId,
    int NumberOfParticipants,
    int MaxNumberOfParticipants,
    Privacy Privacy, 
    ActivityStatus ActivityStatus,
    string? AccessPassword,
    Access AccessOptions,
    AdminInvite AdminInviteCode,
    DateTime EventDate
) : ActivityDTO(
    Id,
    AdminId,
    WorkSpaceId,
    NumberOfParticipants,
    MaxNumberOfParticipants,
    Privacy, 
    ActivityStatus,
    AccessPassword,
    AccessOptions,
    AdminInviteCode
);