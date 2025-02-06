using MediatR;
using WorkEnv.Application.DTO.User;
using WorkEnv.Application.Map;
using WorkEnv.Application.Result;
using WorkEnv.Domain.Interfaces;

namespace WorkEnv.Application.CQRS.User.Query.GetByEmail;

public class GetByEmailQueryHandler : IRequestHandler<GetByEmailQuery, Result<UserDTO>>
{
    private readonly IUnitOfWork _uof;

    public GetByEmailQueryHandler(IUnitOfWork uof)
    {
        _uof = uof;
    }
    
    public async Task<Result<UserDTO>> Handle(GetByEmailQuery request, CancellationToken cancellationToken)
    {
        var user = await _uof.UserRepository.GetByEmailAsync(request.email, cancellationToken);

        if (user is null)
            return Result<UserDTO>.Failure(UserErrors.UserNotFound);

        return Result<UserDTO>.Success(user.ToUserDto());
    }
}