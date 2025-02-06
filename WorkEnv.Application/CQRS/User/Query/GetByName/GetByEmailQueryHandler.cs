using MediatR;
using WorkEnv.Application.DTO.User;
using WorkEnv.Application.Map;
using WorkEnv.Application.Result;
using WorkEnv.Domain.Interfaces;

namespace WorkEnv.Application.CQRS.User.Query.GetByEmail;

public class GetByNameQueryHandler : IRequestHandler<GetByNameQuery, Result<UserDTO>>
{
    private readonly IUnitOfWork _uof;

    public GetByNameQueryHandler(IUnitOfWork uof)
    {
        _uof = uof;
    }
    
    public async Task<Result<UserDTO>> Handle(GetByNameQuery request, CancellationToken cancellationToken)
    {
        var user = await _uof.UserRepository.GetByNameAsync(request.name, cancellationToken);

        if (user is null)
            return Result<UserDTO>.Failure(UserErrors.UserNotFound);

        return Result<UserDTO>.Success(user.ToUserDto());
    }
}