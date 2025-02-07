namespace WorkEnv.Application.DTO.WorkSpace;

public record WorkSpaceDTO(
    Guid WorkSpaceId,
    Guid OwnerId,
    int NumberOfActivities
);