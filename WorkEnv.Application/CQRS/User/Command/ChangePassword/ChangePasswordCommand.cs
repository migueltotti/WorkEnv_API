using MediatR;
using WorkEnv.Application.DTO.User;
using WorkEnv.Application.Result;

namespace WorkEnv.Application.CQRS.User.Command.ChangePassword;

public record ChangePasswordCommand(Guid userId, string oldPassword, string newPassword) : IRequest<Result<UserDTO>>;