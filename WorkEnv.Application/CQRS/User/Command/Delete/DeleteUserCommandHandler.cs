using MediatR;
using WorkEnv.Application.Result;
using WorkEnv.Domain.Interfaces;

namespace WorkEnv.Application.CQRS.User.Command.Delete;

public class DeleteUserCommandHandler(IUnitOfWork uof) : IRequestHandler<DeleteUserCommand, Result.Result>
{
    public async Task<Result.Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await uof.UserRepository.GetByIdAsync(request.userId, cancellationToken);

        if (user is null)
            return Result.Result.Failure(UserErrors.UserNotFound);
        
        uof.UserRepository.Delete(user);

        await uof.CommitChangesAsync(cancellationToken);
        
        return Result.Result.Success();
    }
}