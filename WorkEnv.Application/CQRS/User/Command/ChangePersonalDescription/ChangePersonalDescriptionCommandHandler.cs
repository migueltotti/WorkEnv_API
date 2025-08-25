using MediatR;
using WorkEnv.Application.DTO.User;
using WorkEnv.Application.Map;
using WorkEnv.Application.Result;
using WorkEnv.Domain.Interfaces;

namespace WorkEnv.Application.CQRS.User.Command.ChangePersonalDescription;

public class ChangePersonalDescriptionCommandHandler(IUnitOfWork uof) :
    IRequestHandler<ChangePersonalDescriptionCommand, Result<UserDTO>>
{
    public async Task<Result<UserDTO>> Handle(ChangePersonalDescriptionCommand request,
        CancellationToken cancellationToken)
    {
        var user = await uof.UserRepository.GetByIdAsync(request.userId, cancellationToken);

        if (user is null)
            return Result<UserDTO>.Failure(UserErrors.UserNotFound);

        user.ChangePersonalDescription(request.newDescription);

        uof.UserRepository.Update(user);
        await uof.CommitChangesAsync(cancellationToken);

        return Result<UserDTO>.Success(user.ToUserDto());
    }
}