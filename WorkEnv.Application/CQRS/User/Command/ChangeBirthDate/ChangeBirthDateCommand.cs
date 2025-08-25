using MediatR;
using WorkEnv.Application.DTO.User;
using WorkEnv.Application.Result;

namespace WorkEnv.Application.CQRS.User.Command.ChangeBirthDate;

public record ChangeBirthDateCommand(Guid userId, DateTime newBirthDate) : IRequest<Result<UserDTO>>;
