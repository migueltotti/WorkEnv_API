using WorkEnv.Application.DTO.Role;

namespace WorkEnv.Application.DTO.Map;

public static class RoleMapping
{
    public static RoleDTO ToRoleDTO(this Domain.Entities.Role role)
    {
        return new RoleDTO(
            Guid.NewGuid(),
            role.Name,
            role.Description
        );
    }
}