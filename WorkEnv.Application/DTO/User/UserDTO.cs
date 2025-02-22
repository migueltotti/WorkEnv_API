using MediatR;

namespace WorkEnv.Application.DTO.User;

public record UserDTO(
    Guid UserId,
    string Name,
    string Email,
    DateTime DateBirth
);