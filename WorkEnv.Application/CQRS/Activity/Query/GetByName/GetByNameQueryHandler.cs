using MediatR;
using WorkEnv.Application.DTO.Activity;
using WorkEnv.Application.DTO.Map;
using WorkEnv.Domain.Interfaces;

namespace WorkEnv.Application.CQRS.Activity.Query.GetByName;

public class GetByNameQueryHandler : IRequestHandler<GetByNameQuery, List<ActivityDTO>>
{
    private readonly IUnitOfWork _uof;

    public GetByNameQueryHandler(IUnitOfWork uof)
    {
        _uof = uof;
    }

    public async Task<List<ActivityDTO>> Handle(GetByNameQuery request, CancellationToken cancellationToken)
    {
        var activities = await _uof.ActivityRepository.GetByNameAsync(request.name, cancellationToken);

        var activitiesDTO = activities.Select(a => a.ToActivityDTO());
        
        return activitiesDTO.ToList();
    }
}