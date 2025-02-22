using MediatR;
using WorkEnv.Application.DTO.Activity;
using WorkEnv.Application.DTO.Map;
using WorkEnv.Application.Result;
using WorkEnv.Domain.Interfaces;

namespace WorkEnv.Application.CQRS.WorkSpace.Query.GetAllActivitiesByWorkSpaceId;

public class GetAllActivitiesByWorkSpaceIdQueryHandler : IRequestHandler<GetAllActivitiesByWorkSpaceIdQuery, Result<List<ActivityDTO>>>
{
    public readonly IUnitOfWork _uof;

    public GetAllActivitiesByWorkSpaceIdQueryHandler(IUnitOfWork uof)
    {
        _uof = uof;
    }

    public async Task<Result<List<ActivityDTO>>> Handle(GetAllActivitiesByWorkSpaceIdQuery request, CancellationToken cancellationToken)
    {
        var workSpace = await _uof.WorkSpaceRepository.GetByIdAsync(request.workSpaceId, cancellationToken);

        if (workSpace is null) 
            return Result<List<ActivityDTO>>.Failure(WorkSpaceErrors.WorkSpaceNotFound);
        
        if (!workSpace.OwnerId.Equals(request.ownerId))
            return Result<List<ActivityDTO>>.Failure(WorkSpaceErrors.OwnerIdMismatch);
        
        var owner = await _uof.UserRepository.GetByIdAsync(request.ownerId, cancellationToken);
        
        if (owner is null)
            return Result<List<ActivityDTO>>.Failure(UserErrors.UserNotFound);

        var activitiesDTO = workSpace.Activities
            .Select(a => a.ToActivityDTO())
            .ToList();
        
        return Result<List<ActivityDTO>>.Success(activitiesDTO);
    }
}