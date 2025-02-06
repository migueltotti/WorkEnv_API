using MediatR;
using WorkEnv.Application.DTO.User;
using WorkEnv.Application.Map;
using WorkEnv.Domain.Interfaces;

namespace WorkEnv.Application.CQRS.User.Query.GetAllQuery;

public class GetAllQueryHandler : IRequestHandler<GetAllQuery, List<UserDTO>>
{
    private readonly IUnitOfWork _uof;

    public GetAllQueryHandler(IUnitOfWork uof)
    {
        _uof = uof;
    }

    public async Task<List<UserDTO>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        var users = await _uof.UserRepository.GetAllAsync(cancellationToken);

        var usersDTO = users.Select(u => u.ToUserDto());

        return usersDTO.ToList();
    }
}