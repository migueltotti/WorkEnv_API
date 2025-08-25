using MediatR;
using WorkEnv.Application.DTO.User;
using WorkEnv.Application.Result;

namespace WorkEnv.Application.CQRS.User.Command.ChangeProfilePicture;

public record ChangeProfilePictureCommand(Guid userId, string newPersonalDescription) : IRequest<Result<UserDTO>>;