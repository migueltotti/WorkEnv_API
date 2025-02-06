using MediatR;
using WorkEnv.Application.DTO.User;
using WorkEnv.Application.Map;
using WorkEnv.Application.Result;
using WorkEnv.Domain.Interfaces;

namespace WorkEnv.Application.CQRS.User.Query.GetById;

public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, Result<UserDTO>>
{
    private readonly IUserRepository _userRepository;

    public GetByIdQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<UserDTO>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.userId, cancellationToken);
        
        if(user is null)
            return Result<UserDTO>.Failure(UserErrors.UserNotFound);

        return Result<UserDTO>.Success(user.ToUserDto());
    }
}