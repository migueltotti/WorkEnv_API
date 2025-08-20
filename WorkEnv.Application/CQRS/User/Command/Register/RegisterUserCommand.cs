using MediatR;
using WorkEnv.Application.DTO.User;
using WorkEnv.Application.Result;
using WorkEnv.Domain.Enum;

namespace WorkEnv.Application.CQRS.User.Command.Register;

public record RegisterUserCommand(
    string name,
    string email,
    string password,
    string cpfCnpj,
    DateTime dateBirth,
    string profilePicture,
    string personalDescription,
    Privacy privacy
) : IRequest<Result<UserDTO>>;
