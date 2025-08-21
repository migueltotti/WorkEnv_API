using WorkEnv.Application.DTO.Activity;
using WorkEnv.Domain.Enum;
using WorkEnv.Domain.ValueObjects;
using TaskStatus = WorkEnv.Domain.Enum.TaskStatus;

namespace WorkEnv.Application.DTO.Task;

public record EventDTO(
    Guid Id,
    Guid? AdminId,
    Guid WorkSpaceId,
    int NumberOfParticipants,
    int MaxNumberOfParticipants,
    Privacy Privacy, 
    TaskStatus TaskStatus,
    string? AccessPassword,
    EventAccessOption EventAccessOptionOptions,
    AdminInvite AdminInviteCode,
    DateTime EventDate
) : ActivityDTO(
    Id,
    AdminId,
    WorkSpaceId,
    NumberOfParticipants,
    MaxNumberOfParticipants,
    Privacy, 
    TaskStatus,
    AccessPassword,
    EventAccessOptionOptions,
    AdminInviteCode
);