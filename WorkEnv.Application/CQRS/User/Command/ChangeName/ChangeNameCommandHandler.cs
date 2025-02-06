using MediatR;
using WorkEnv.Application.DTO.User;
using WorkEnv.Application.Map;
using WorkEnv.Application.Result;
using WorkEnv.Application.Extensions;
using WorkEnv.Domain.Interfaces;

namespace WorkEnv.Application.CQRS.User.Command.ChangeName;

public class ChangeNameCommandHandler : IRequestHandler<ChangeNameCommand, Result<UserDTO>>
{
    private readonly IUnitOfWork _uof;

    public ChangeNameCommandHandler(IUnitOfWork uof)
    {
        _uof = uof;
    }

    public async Task<Result<UserDTO>> Handle(ChangeNameCommand request, CancellationToken cancellationToken)
    {
        var user = await _uof.UserRepository.GetByIdAsync(request.userId, cancellationToken);
        
        if(user is null)
            return Result<UserDTO>.Failure(UserErrors.UserNotFound);
        
        if(!request.newName.IsValidName())
            return Result<UserDTO>.Failure(UserErrors.IncorrectEmailFormat);
        
        user.ChangeName(request.newName);

        await _uof.CommitChangesAsync(cancellationToken);

        return Result<UserDTO>.Success(user.ToUserDto());
    }
}