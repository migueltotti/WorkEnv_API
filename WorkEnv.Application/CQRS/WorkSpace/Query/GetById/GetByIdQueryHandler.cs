using MediatR;
using WorkEnv.Application.DTO.WorkSpace;
using WorkEnv.Application.Map;
using WorkEnv.Application.Result;
using WorkEnv.Domain.Interfaces;

namespace WorkEnv.Application.CQRS.WorkSpace.Query.GetById;

public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, Result<WorkSpaceDTO>>
{
    private readonly IUnitOfWork _uof;

    public GetByIdQueryHandler(IUnitOfWork uof)
    {
        _uof = uof;
    }

    public async Task<Result<WorkSpaceDTO>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var workSpace = await _uof.WorkSpaceRepository.GetByIdAsync(request.workSpaceId, cancellationToken);
        
        if(workSpace is null)
            return Result<WorkSpaceDTO>.Failure(WorkSpaceErrors.WorkSpaceNotFound);

        return Result<WorkSpaceDTO>.Success(workSpace.ToWorkSpaceDto());
    }
}