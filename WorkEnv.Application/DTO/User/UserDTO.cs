using MediatR;
using WorkEnv.Domain.Enum;

namespace WorkEnv.Application.DTO.User;

public record UserDTO(
    Guid UserId,
    string Name,
    string Email,
    DateTimeOffset DateBirth,
    string? ProfilePicture,
    Privacy Privacy,
    string? PersonalDescription
);