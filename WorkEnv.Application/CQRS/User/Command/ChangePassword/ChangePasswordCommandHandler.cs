using MediatR;
using WorkEnv.Application.DTO.User;
using WorkEnv.Application.Map;
using WorkEnv.Application.Result;
using WorkEnv.Application.Extensions;
using WorkEnv.Domain.Interfaces;

namespace WorkEnv.Application.CQRS.User.Command.ChangePassword;

public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result<UserDTO>>
{
    private readonly IUnitOfWork _uof;

    public ChangePasswordCommandHandler(IUnitOfWork uof)
    {
        _uof = uof;
    }

    public async Task<Result<UserDTO>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _uof.UserRepository.GetByIdAsync(request.userId, cancellationToken);
        
        if(user is null)
            return Result<UserDTO>.Failure(UserErrors.UserNotFound);
        
        if(!user.Password.Equals(request.oldPassword))
            return Result<UserDTO>.Failure(UserErrors.OldPasswordMismatch); 
        
        if(user.Password.Equals(request.newPassword))
            return Result<UserDTO>.Failure(UserErrors.EqualPassword); 
        
        if(!PasswordValidation.IsValidPassword(request.newPassword))
            return Result<UserDTO>.Failure(UserErrors.IncorrectEmailFormat);
        
        user.ChangePassword(request.oldPassword, request.newPassword);

        await _uof.CommitChangesAsync(cancellationToken);

        return Result<UserDTO>.Success(user.ToUserDto());
    }
}