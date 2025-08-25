using MediatR;
using WorkEnv.Application.DTO.User;
using WorkEnv.Application.Result;

namespace WorkEnv.Application.CQRS.User.Command.ChangePersonalDescription;

public record ChangePersonalDescriptionCommand(Guid userId, string newDescription)
    : IRequest<Result<UserDTO>>;