using MediatR;
using WorkEnv.Application.DTO.User;
using WorkEnv.Application.Result;

namespace WorkEnv.Application.CQRS.User.Query.GetById;

public record GetByIdQuery(Guid userId) : IRequest<Result<UserDTO>>;