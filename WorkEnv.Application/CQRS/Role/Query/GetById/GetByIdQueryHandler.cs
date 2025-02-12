using MediatR;
using WorkEnv.Application.DTO.Map;
using WorkEnv.Application.DTO.Message;
using WorkEnv.Application.DTO.Role;
using WorkEnv.Application.Result;
using WorkEnv.Domain.Interfaces;

namespace WorkEnv.Application.CQRS.Role.Query.GetById;

public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, Result<RoleDTO>>
{
    private readonly IUnitOfWork _uof;

    public GetByIdQueryHandler(IUnitOfWork uof)
    {
        _uof = uof;
    }

    public async Task<Result<RoleDTO>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var role = await _uof.RoleRepository.GetByIdAsync(request.roleId, cancellationToken);

        if (role is null)
            return Result<RoleDTO>.Failure(RoleErrors.RoleNotFound);
        
        return Result<RoleDTO>.Success(role.ToRoleDTO());
    }
}