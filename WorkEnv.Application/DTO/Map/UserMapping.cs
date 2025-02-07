using WorkEnv.Application.DTO.User;
using WorkEnv.Domain.Entities;

namespace WorkEnv.Application.Map;

public static class UserMapping
{
    public static UserDTO ToUserDto(this User user)
    {
        return new UserDTO(
            user.UserId,
            user.Name,
            user.Email,
            user.DateBirth
        );
    }
}