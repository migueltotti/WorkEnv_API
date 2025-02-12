using System.Globalization;
using MediatR;
using WorkEnv.Application.DTO.User;
using WorkEnv.Application.Map;
using WorkEnv.Domain.Interfaces;
using WorkEnv.Application.Result;

namespace WorkEnv.Application.CQRS.Task.Query.GetAllUsers;

public class GetActivityUsersByIdQueryHandler : IRequestHandler<GetActivityUsersByIdQuery, Result<List<UserDTO>>>
{
    private readonly IUnitOfWork _uof;

    public GetActivityUsersByIdQueryHandler(IUnitOfWork uof)
    {
        _uof = uof;
    }

    public async Task<Result<List<UserDTO>>> Handle(GetActivityUsersByIdQuery request, CancellationToken cancellationToken)
    {
        var task = await _uof.TaskRepository.GetByIdWithUsersAsync(request.taskId, cancellationToken);

        if (task is null)
            return Result<List<UserDTO>>.Failure(ActivityErrors.ActivityNotFound);

        var users = task.UserActivities
            .Select(u => u.User.ToUserDto());
        
        return Result<List<UserDTO>>.Success(users.ToList());
    }
}