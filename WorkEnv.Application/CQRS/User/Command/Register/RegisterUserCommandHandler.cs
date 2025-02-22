using MediatR;
using WorkEnv.Application.DTO.User;
using WorkEnv.Application.Map;
using WorkEnv.Application.Result;
using WorkEnv.Domain.Interfaces;
using WorkEnv.Domain.Entities;

namespace WorkEnv.Application.CQRS.User.Command.Register;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<UserDTO>>
{
    private readonly IUnitOfWork _uof;

    public RegisterUserCommandHandler(IUnitOfWork uof)
    {
        _uof = uof;
    }

    public async Task<Result<UserDTO>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        // Validations
        if (request.Name is null ||
            request.Email is null ||
            request.Password is null)
            return Result<UserDTO>.Failure(UserErrors.DataIsNull);

        var emailExists = await _uof.UserRepository.VerifyEmail(request.Email, cancellationToken);
        
        if(emailExists)
            return Result<UserDTO>.Failure(UserErrors.EmailExists);
        
        var user = new Domain.Entities.User(
            Guid.NewGuid(),
            request.Name,
            request.Email,
            request.Password,
            request.DateBirth
        );

        await _uof.UserRepository.AddAsync(user, cancellationToken);

        await _uof.CommitChangesAsync(cancellationToken);

        return Result<UserDTO>.Success(user.ToUserDto());
    }
}