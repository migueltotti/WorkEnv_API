using MediatR;
using WorkEnv.Application.Result;
using WorkEnv.Domain.Interfaces;

namespace WorkEnv.Application.CQRS.WorkSpace.Command.Delete;

public class DeleteCommandHandler : IRequestHandler<DeleteCommand, Result.Result>
{
    private readonly IUnitOfWork _uof;

    public DeleteCommandHandler(IUnitOfWork uof)
    {
        _uof = uof;
    }

    public async Task<Result.Result> Handle(DeleteCommand request, CancellationToken cancellationToken)
    {
        var owner = await _uof.UserRepository.GetByIdAsync(request.ownerId, cancellationToken);
        
        if(owner is null)
            return Result.Result.Failure(UserErrors.UserNotFound);
        
        var workSpace = await _uof.WorkSpaceRepository.GetByIdAsync(request.workSpaceId, cancellationToken);
        
        if(workSpace is null)
            return Result.Result.Failure(WorkSpaceErrors.WorkSpaceNotFound);
        
        if(!workSpace.OwnerId.Equals(request.ownerId))
            return Result.Result.Failure(WorkSpaceErrors.OwnerIdMismatch);
        
        _uof.WorkSpaceRepository.Delete(workSpace);
        
        await _uof.CommitChangesAsync(cancellationToken);
        
        return Result.Result.Success();
    }
}