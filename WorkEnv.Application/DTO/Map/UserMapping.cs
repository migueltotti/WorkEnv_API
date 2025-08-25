using WorkEnv.Application.DTO.User;
using WorkEnv.Domain.Entities;
using WorkEnv.Domain.Enum;

namespace WorkEnv.Application.Map;

public static class UserMapping
{
    public static UserDTO ToUserDto(this User user)
    {
        return new UserDTO(
            user.Id,
            user.Name,
            user.Email,
            user.DateBirth,
            user.ProfilePicture,
            user.Privacy,
            user.PersonalDescription
        );
    }
}