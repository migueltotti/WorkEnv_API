using MediatR;
using WorkEnv.Application.DTO.WorkSpace;
using WorkEnv.Application.Map;
using WorkEnv.Application.Result;
using WorkEnv.Domain.Interfaces;

namespace WorkEnv.Application.CQRS.WorkSpace.Query.GetAllByUserId;

public class GetAllByUserIdQueryHandler : IRequestHandler<GetAllByUserIdQuery, Result<List<WorkSpaceDTO>>>
{
    private readonly IUnitOfWork _uof;

    public GetAllByUserIdQueryHandler(IUnitOfWork uof)
    {
        _uof = uof;
    }

    public async Task<Result<List<WorkSpaceDTO>>> Handle(GetAllByUserIdQuery request, CancellationToken cancellationToken)
    {
         var user = await _uof.UserRepository.GetByIdAsync(request.userId, cancellationToken);
         
         if(user is null) return Result<List<WorkSpaceDTO>>.Failure(UserErrors.UserNotFound);
         
         var workSpaces = await _uof.WorkSpaceRepository.GetAllByUserIdAsync(request.userId, cancellationToken);

         var workSpacesDTO = workSpaces.Select(w => w.ToWorkSpaceDto()).ToList();
         
         return Result<List<WorkSpaceDTO>>.Success(workSpacesDTO);
    }
}