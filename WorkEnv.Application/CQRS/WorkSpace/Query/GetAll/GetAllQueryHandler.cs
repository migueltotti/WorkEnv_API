using MediatR;
using WorkEnv.Application.DTO.WorkSpace;
using WorkEnv.Application.Map;
using WorkEnv.Domain.Interfaces;

namespace WorkEnv.Application.CQRS.WorkSpace.Query.GetAll;

public class GetAllQueryHandler : IRequestHandler<GetAllQuery, List<WorkSpaceDTO>>
{
    private readonly IUnitOfWork _uof;

    public GetAllQueryHandler(IUnitOfWork uof)
    {
        _uof = uof;
    }
    
    public async Task<List<WorkSpaceDTO>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        var workSpaces = await _uof.WorkSpaceRepository.GetAllAsync(cancellationToken);
        
        var workSpacesDto = workSpaces
            .Select(w => w.ToWorkSpaceDto())
            .ToList();
        
        return workSpacesDto;
    }
}