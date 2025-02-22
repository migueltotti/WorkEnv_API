using MediatR;
using WorkEnv.Application.DTO.Message;
using WorkEnv.Application.DTO.Role;
using WorkEnv.Application.Result;

namespace WorkEnv.Application.CQRS.Role.Query.GetById;

public record GetByIdQuery(Guid roleId) : IRequest<Result<RoleDTO>>;