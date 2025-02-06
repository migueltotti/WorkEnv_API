using MediatR;
using WorkEnv.Application.DTO.User;
using WorkEnv.Application.Result;

namespace WorkEnv.Application.CQRS.User.Query.GetByEmail;

public record GetByNameQuery(string name) : IRequest<Result<UserDTO>>;
