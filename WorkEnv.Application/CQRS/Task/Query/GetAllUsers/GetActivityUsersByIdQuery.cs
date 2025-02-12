using MediatR;
using WorkEnv.Application.DTO.User;
using WorkEnv.Application.Result;

namespace WorkEnv.Application.CQRS.Task.Query.GetAllUsers;

public record GetActivityUsersByIdQuery(Guid taskId) : IRequest<Result<List<UserDTO>>>;