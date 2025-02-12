using MediatR;
using WorkEnv.Application.DTO.User;
using WorkEnv.Application.Map;
using WorkEnv.Domain.Interfaces;
using WorkEnv.Application.Result;

namespace WorkEnv.Application.CQRS.Event.Query.GetAllUsers;

public class GetActivityUsersByIdQueryHandler : IRequestHandler<GetActivityUsersByIdQuery, Result<List<UserDTO>>>
{
    private readonly IUnitOfWork _uof;

    public GetActivityUsersByIdQueryHandler(IUnitOfWork uof)
    {
        _uof = uof;
    }

    public async Task<Result<List<UserDTO>>> Handle(GetActivityUsersByIdQuery request, CancellationToken cancellationToken)
    {
        var @event = await _uof.EventRepository.GetByIdWithUsersAsync(request.eventId, cancellationToken);

        if (@event is null)
            return Result<List<UserDTO>>.Failure(ActivityErrors.ActivityNotFound);

        var users = @event.UserActivities
            .Select(u => u.User.ToUserDto());
        
        return Result<List<UserDTO>>.Success(users.ToList());
    }
}