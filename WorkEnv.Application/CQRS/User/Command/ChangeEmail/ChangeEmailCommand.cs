using MediatR;
using WorkEnv.Application.DTO.User;
using WorkEnv.Application.Result;

namespace WorkEnv.Application.CQRS.User.Command.ChangeEmail;

public record ChangeEmailCommand(Guid userId, string newEmail) : IRequest<Result<UserDTO>>;