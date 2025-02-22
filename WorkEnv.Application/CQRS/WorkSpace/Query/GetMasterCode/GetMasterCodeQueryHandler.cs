using MediatR;
using WorkEnv.Application.Result;
using WorkEnv.Domain.Interfaces;

namespace WorkEnv.Application.CQRS.WorkSpace.Query.GetMasterCode;

public class GetMasterCodeQueryHandler : IRequestHandler<GetMasterCodeQuery, Result<string>>
{
    private readonly IUnitOfWork _uof;

    public GetMasterCodeQueryHandler(IUnitOfWork uof)
    {
        _uof = uof;
    }
    
    public async Task<Result<string>> Handle(GetMasterCodeQuery request, CancellationToken cancellationToken)
    {
        var workSpace = await _uof.WorkSpaceRepository.GetByIdAsync(request.workSpaceId, cancellationToken);
        
        if(workSpace is null)
            return Result<string>.Failure(WorkSpaceErrors.WorkSpaceNotFound);

        var masterCode = workSpace.GetMasterCode(request.ownerId);
        
        if(masterCode is null)
            return Result<string>.Failure(WorkSpaceErrors.OwnerIdMismatch);

        return Result<string>.Success(masterCode);
    }
}