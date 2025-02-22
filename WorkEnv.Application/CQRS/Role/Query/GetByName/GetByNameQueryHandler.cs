using MediatR;
using WorkEnv.Application.CQRS.Message.Query.GetByTitle;
using WorkEnv.Application.CQRS.Role.Query.GetByName;
using WorkEnv.Application.DTO.Map;
using WorkEnv.Application.DTO.Message;
using WorkEnv.Application.DTO.Role;
using WorkEnv.Application.Result;
using WorkEnv.Domain.Interfaces;

namespace WorkEnv.Application.CQRS.Role.Query.GetByTitle;

public class GetByNameQueryHandler : IRequestHandler<GetByNameQuery, Result<RoleDTO>>
{
    private readonly IUnitOfWork _uof;

    public GetByNameQueryHandler(IUnitOfWork uof)
    {
        _uof = uof;
    }

    public async Task<Result<RoleDTO>> Handle(GetByNameQuery request, CancellationToken cancellationToken)
    {
        var role = await _uof.RoleRepository.GetByNameAsync(request.roleName, cancellationToken);

        if (role is null)
            return Result<RoleDTO>.Failure(MessageErrors.MessageNotFound);
        
        return Result<RoleDTO>.Success(role.ToRoleDTO());
    }
}