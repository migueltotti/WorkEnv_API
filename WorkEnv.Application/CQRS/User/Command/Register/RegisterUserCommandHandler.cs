using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using WorkEnv.Application.CQRS.Auth.Register;
using WorkEnv.Application.DTO.User;
using WorkEnv.Application.Map;
using WorkEnv.Application.Result;
using WorkEnv.Domain.Interfaces;
using WorkEnv.Domain.Entities;

namespace WorkEnv.Application.CQRS.User.Command.Register;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<UserDTO>>
{
    private readonly IUnitOfWork _uof;
    private readonly IValidator<RegisterUserCommand> _validator;
    private readonly IEncryptService _encryptService;
    private readonly ISender _sender;
    private readonly ILogger<RegisterUserCommandHandler> _logger;

    public RegisterUserCommandHandler(IUnitOfWork uof, IValidator<RegisterUserCommand> validator, IEncryptService encryptService, ISender sender, ILogger<RegisterUserCommandHandler> logger)
    {
        _uof = uof;
        _validator = validator;
        _encryptService = encryptService;
        _sender = sender;
        _logger = logger;
    }

    public async Task<Result<UserDTO>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var decryptedPassword = _encryptService.Decrypt(request.password);
        
        request = request with { password = decryptedPassword };
        
        var result = await _validator.ValidateAsync(request, cancellationToken);
        
        if (!result.IsValid)
            return Result<UserDTO>.Failure(UserErrors.IncorrectFormatData, result.Errors);

        var emailExists = await _uof.UserRepository.VerifyEmail(request.email, cancellationToken);
        
        if(emailExists)
            return Result<UserDTO>.Failure(UserErrors.EmailExists);
        
        var cpfOrCnpjExists = await _uof.UserRepository.VerifyCpfOrCnpj(request.cpfCnpj, cancellationToken);
        
        if(cpfOrCnpjExists)
            return Result<UserDTO>.Failure(UserErrors.CpfOrCnpjExists);
        
        var newEncryptedPassword = _encryptService.Encrypt(request.password);
        
        var newUser = new Domain.Entities.User(
            Guid.NewGuid(),
            name: request.name,
            email: request.email,
            password: newEncryptedPassword,
            cpfCnpj: request.cpfCnpj,
            dateBirth: request.dateBirth,
            profilePicture: request.profilePicture,
            personalDescription: request.personalDescription,
            privacy: request.privacy
        );
        
        await _uof.UserRepository.AddAsync(newUser, cancellationToken);
        
        // Register Auth User
        var newAuthUserCommand = new RegisterAuthUserCommand(newUser.Id.ToString(), newUser.Name, newUser.Email, decryptedPassword);

        var registerAuthUserResult = await _sender.Send(newAuthUserCommand, cancellationToken);
        
        if(!registerAuthUserResult.IsSuccess)
            return Result<UserDTO>.Failure(registerAuthUserResult.Error);

        await _uof.CommitChangesAsync(cancellationToken);

        return Result<UserDTO>.Success(newUser.ToUserDto());
    }
}