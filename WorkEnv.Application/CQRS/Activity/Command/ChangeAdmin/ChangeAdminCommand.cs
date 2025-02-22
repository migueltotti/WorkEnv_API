using MediatR;

namespace WorkEnv.Application.CQRS.Activity.Command.ChangeAdmin;

public record ChangeAdminCommand(
    Guid activityId, Guid adminOrOwnerId, Guid newAdminId) : IRequest<Result.Result>; 