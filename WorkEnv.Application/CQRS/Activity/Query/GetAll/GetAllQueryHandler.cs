using MediatR;
using WorkEnv.Application.DTO.Activity;
using WorkEnv.Application.DTO.Map;
using WorkEnv.Application.Map;
using WorkEnv.Domain.Interfaces;

namespace WorkEnv.Application.CQRS.Activity.Query.GetAll;

public class GetAllQueryHandler : IRequestHandler<GetAllQuery, List<ActivityDTO>>
{
    private readonly IUnitOfWork _uof;

    public GetAllQueryHandler(IUnitOfWork uof)
    {
        _uof = uof;
    }

    public async Task<List<ActivityDTO>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        var events = await _uof.EventRepository.GetAllAsync(cancellationToken);
        var tasks = await _uof.TaskRepository.GetAllAsync(cancellationToken);

        var eventsDto = events.Select(e => e.ToEventDto()).ToList();
        var taskDto = tasks.Select(t => t.ToTaskDto()).ToList();
        
        var activities = new List<ActivityDTO>();
        activities.AddRange(eventsDto);
        activities.AddRange(taskDto);
        
        return activities;
    }
}