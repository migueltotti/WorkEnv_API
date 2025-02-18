using MediatR;
using WorkEnv.Application.DTO.Role;
using WorkEnv.Application.Result;

namespace WorkEnv.Application.CQRS.Role.Command.Create;

public record CreateCommand(
    string name,
    string description    
) : IRequest<Result<RoleDTO>>;