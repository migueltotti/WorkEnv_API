using MediatR;
using WorkEnv.Application.DTO.User;
using WorkEnv.Application.Result;

namespace WorkEnv.Application.CQRS.Event.Query.GetAllUsers;

public record GetActivityUsersByIdQuery(Guid eventId) : IRequest<Result<List<UserDTO>>>;