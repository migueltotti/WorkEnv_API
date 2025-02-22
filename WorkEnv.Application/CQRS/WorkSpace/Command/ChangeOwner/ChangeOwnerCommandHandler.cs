using MediatR;
using WorkEnv.Application.Result;
using WorkEnv.Domain.Interfaces;

namespace WorkEnv.Application.CQRS.WorkSpace.Command.ChangeOwner;

public class ChangeOwnerCommandHandler : IRequestHandler<ChangeOwnerCommand, Result.Result>
{
    private readonly IUnitOfWork _uof;

    public ChangeOwnerCommandHandler(IUnitOfWork uof)
    {
        _uof = uof;
    }

    public async Task<Result.Result> Handle(ChangeOwnerCommand request, CancellationToken cancellationToken)
    {
        var workSpace = await _uof.WorkSpaceRepository.GetByIdAsync(request.workSpaceId, cancellationToken);

        if (workSpace is null)
            return Result.Result.Failure(WorkSpaceErrors.WorkSpaceNotFound);
        
        if(!workSpace.OwnerId.Equals(request.oldOwnerId))
            return Result.Result.Failure(WorkSpaceErrors.OwnerIdMismatch);
        
        var oldOwner = await _uof.UserRepository.GetByIdAsync(request.oldOwnerId, cancellationToken);

        if (oldOwner is null)
            return Result.Result.Failure(UserErrors.UserNotFound);
        
        var newOwner = await _uof.UserRepository.GetByIdAsync(request.newOwnerId, cancellationToken);

        if (newOwner is null)
            return Result.Result.Failure(UserErrors.UserNotFound);
        
        workSpace.ChangeOwner(oldOwner.UserId, newOwner.UserId);
        
        _uof.WorkSpaceRepository.Update(workSpace);
        await _uof.CommitChangesAsync(cancellationToken);

        return Result.Result.Success();
    }
}