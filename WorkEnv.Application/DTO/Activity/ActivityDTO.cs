using WorkEnv.Domain.Enum;
using WorkEnv.Domain.ValueObjects;
using TaskStatus = WorkEnv.Domain.Enum.TaskStatus;

namespace WorkEnv.Application.DTO.Activity;

public record ActivityDTO(
    Guid Id,
    Guid? AdminId,
    Guid WorkSpaceId,
    int NumberOfParticipants,
    int MaxNumberOfParticipants,
    Privacy Privacy, 
    TaskStatus TaskStatus,
    string? AccessPassword,
    EventAccessOption EventAccessOptionOptions,
    AdminInvite AdminInviteCode
);