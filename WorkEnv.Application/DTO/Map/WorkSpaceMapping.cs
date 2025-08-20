using WorkEnv.Application.DTO.WorkSpace;
using WorkEnv.Domain.Entities;

namespace WorkEnv.Application.Map;

public static class WorkSpaceMapping
{
    public static WorkSpaceDTO ToWorkSpaceDto(this WorkSpace workSpace)
    {
        return new WorkSpaceDTO(
            workSpace.Id,
            workSpace.OwnerId,
            workSpace.NumberOfActivities
            );
    }
}