using MediatR;
using WorkEnv.Application.DTO.User;
using WorkEnv.Application.Map;
using WorkEnv.Application.Result;
using WorkEnv.Domain.Interfaces;

namespace WorkEnv.Application.CQRS.User.Command.ChangeProfilePicture;

public class ChangeProfilePictureCommandHandler(IUnitOfWork uof) : IRequestHandler<ChangeProfilePictureCommand, Result<UserDTO>>
{
    public async Task<Result<UserDTO>> Handle(ChangeProfilePictureCommand request, CancellationToken cancellationToken)
    {
        var user = await uof.UserRepository.GetByIdAsync(request.userId, cancellationToken);
        
        if(user is null)
            return Result<UserDTO>.Failure(UserErrors.UserNotFound);
        
        user.ChangePersonalDescription(request.newPersonalDescription);
        
        uof.UserRepository.Update(user);
        await uof.CommitChangesAsync(cancellationToken);

        return Result<UserDTO>.Success(user.ToUserDto());
    }
}