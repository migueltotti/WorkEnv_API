using MediatR;
using WorkEnv.Application.DTO.User;
using WorkEnv.Application.Result;

namespace WorkEnv.Application.CQRS.User.Command.Register;

public record RegisterUserCommand(
    string Name,
    string Email,
    string Password,
    DateTime DateBirth) : IRequest<Result<UserDTO>>;
