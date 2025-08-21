using MediatR;
using WorkEnv.Application.DTO.Map;
using WorkEnv.Application.DTO.Role;
using WorkEnv.Application.Result;
using WorkEnv.Domain.Interfaces;

namespace WorkEnv.Application.CQRS.Role.Command.Create;

public class CreateCommandHandler : IRequestHandler<CreateCommand, Result<RoleDTO>>
{
    private readonly IUnitOfWork _uof;

    public CreateCommandHandler(IUnitOfWork uof)
    {
        _uof = uof;
    }

    public async Task<Result<RoleDTO>> Handle(CreateCommand request, CancellationToken cancellationToken)
    {
        var role = new Domain.Entities.Role(
            request.name,
            request.description,
            Guid.NewGuid()
        );
        
        await _uof.RoleRepository.AddAsync(role, cancellationToken);
        await _uof.CommitChangesAsync(cancellationToken);

        return Result<RoleDTO>.Success(role.ToRoleDTO());
    }
}