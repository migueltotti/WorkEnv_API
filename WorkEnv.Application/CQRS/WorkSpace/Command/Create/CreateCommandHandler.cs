using MediatR;
using WorkEnv.Application.DTO.WorkSpace;
using WorkEnv.Application.Map;
using WorkEnv.Application.Result;
using WorkEnv.Domain.Interfaces;

namespace WorkEnv.Application.CQRS.WorkSpace.Command.Create;

public class CreateCommandHandler : IRequestHandler<CreateCommand, Result<WorkSpaceDTO>>
{
    private readonly IUnitOfWork _uof;

    public CreateCommandHandler(IUnitOfWork uof)
    {
        _uof = uof;
    }

    public async Task<Result<WorkSpaceDTO>> Handle(CreateCommand request, CancellationToken cancellationToken)
    {
        var user = await _uof.UserRepository.GetByIdAsync(request.ownerId, cancellationToken);

        if (user is null)
            return Result<WorkSpaceDTO>.Failure(UserErrors.UserNotFound);
        
        if(string.IsNullOrEmpty(request.masterCode))
            return Result<WorkSpaceDTO>.Failure(WorkSpaceErrors.MasterCodeEmptyOrNull);

        var workSpace = new Domain.Entities.WorkSpace(
            "",
            "",
            request.ownerId
            );
        
        await _uof.WorkSpaceRepository.AddAsync(workSpace, cancellationToken);

        await _uof.CommitChangesAsync(cancellationToken);
        
        return Result<WorkSpaceDTO>.Success(workSpace.ToWorkSpaceDto());
    }
}