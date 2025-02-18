using MediatR;
using WorkEnv.Application.DTO.Role;
using WorkEnv.Application.Result;
using WorkEnv.Domain.Interfaces;

namespace WorkEnv.Application.CQRS.Role.Command.Delete;

public class DeleteCommandHandler : IRequestHandler<DeleteCommand, Result.Result>
{
    private readonly IUnitOfWork _uof;

    public DeleteCommandHandler(IUnitOfWork uof)
    {
        _uof = uof;
    }

    public async Task<Result.Result> Handle(DeleteCommand request, CancellationToken cancellationToken)
    {
        var role = await _uof.RoleRepository.GetByIdAsync(request.roleId, cancellationToken);

        if (role is null)
            return Result.Result.Failure(RoleErrors.RoleNotFound);
        
        _uof.RoleRepository.Delete(role);
        await _uof.CommitChangesAsync(cancellationToken);
        
        return Result.Result.Success();
    }
}