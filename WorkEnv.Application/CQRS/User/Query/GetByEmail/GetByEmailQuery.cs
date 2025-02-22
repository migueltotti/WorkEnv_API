using MediatR;
using WorkEnv.Application.DTO.User;
using WorkEnv.Application.Result;

namespace WorkEnv.Application.CQRS.User.Query.GetByEmail;

public record GetByEmailQuery(string email) : IRequest<Result<UserDTO>>;
