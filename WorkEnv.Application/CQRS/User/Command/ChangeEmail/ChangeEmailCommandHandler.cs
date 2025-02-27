using MediatR;
using WorkEnv.Application.DTO.User;
using WorkEnv.Application.Map;
using WorkEnv.Application.Result;
using WorkEnv.Application.Extensions;
using WorkEnv.Domain.Interfaces;

namespace WorkEnv.Application.CQRS.User.Command.ChangeEmail;

public class ChangeEmailCommandHandler : IRequestHandler<ChangeEmailCommand, Result<UserDTO>>
{
    private readonly IUnitOfWork _uof;

    public ChangeEmailCommandHandler(IUnitOfWork uof)
    {
        _uof = uof;
    }

    public async Task<Result<UserDTO>> Handle(ChangeEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await _uof.UserRepository.GetByIdAsync(request.userId, cancellationToken);
        
        if(user is null)
            return Result<UserDTO>.Failure(UserErrors.UserNotFound);

        if (user.Email.Equals(request.newEmail))
            return Result<UserDTO>.Failure(UserErrors.EqualEmail);
        
        var emailExists = await _uof.UserRepository.VerifyEmail(request.newEmail, cancellationToken);
        
        if(emailExists)
            return Result<UserDTO>.Failure(UserErrors.EmailExists);
        
        if(!request.newEmail.IsValidEmail())
            return Result<UserDTO>.Failure(UserErrors.IncorrectEmailFormat);
        
        user.ChangeEmail(request.newEmail);

        await _uof.CommitChangesAsync(cancellationToken);

        return Result<UserDTO>.Success(user.ToUserDto());
    }
}