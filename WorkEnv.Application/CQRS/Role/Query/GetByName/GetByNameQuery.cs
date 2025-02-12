using MediatR;
using WorkEnv.Application.DTO.Message;
using WorkEnv.Application.DTO.Role;
using WorkEnv.Application.Result;

namespace WorkEnv.Application.CQRS.Role.Query.GetByName;

public record GetByNameQuery(string roleName) : IRequest<Result<RoleDTO>>;