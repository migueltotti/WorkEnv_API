using MediatR;
using WorkEnv.Application.Result;
using WorkEnv.Domain.Interfaces;

namespace WorkEnv.Application.CQRS.User.Command.Delete;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result.Result>
{
    private readonly IUnitOfWork _uof;

    public DeleteUserCommandHandler(IUnitOfWork uof)
    {
        _uof = uof;
    }

    public async Task<Result.Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _uof.UserRepository.GetByIdAsync(request.userId, cancellationToken);

        if (user is null)
            return Result.Result.Failure(UserErrors.UserNotFound);
        
        _uof.UserRepository.Delete(user);

        await _uof.CommitChangesAsync(cancellationToken);
        
        return Result.Result.Success();
    }
}