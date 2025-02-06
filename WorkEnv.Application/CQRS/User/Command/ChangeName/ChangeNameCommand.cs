using MediatR;
using WorkEnv.Application.DTO.User;
using WorkEnv.Application.Result;

namespace WorkEnv.Application.CQRS.User.Command.ChangeName;

public record ChangeNameCommand(Guid userId, string newName) : IRequest<Result<UserDTO>>;