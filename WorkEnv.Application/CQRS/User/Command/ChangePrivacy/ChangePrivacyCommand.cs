using MediatR;
using WorkEnv.Application.DTO.User;
using WorkEnv.Domain.Enum;
using WorkEnv.Application.Result;

namespace WorkEnv.Application.CQRS.User.Command.ChangePrivacy;

public record ChangePrivacyCommand(Guid userId, Privacy NewPrivacy) : IRequest<Result<UserDTO>>;