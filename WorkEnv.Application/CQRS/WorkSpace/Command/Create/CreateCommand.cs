using MediatR;
using WorkEnv.Application.DTO.WorkSpace;
using WorkEnv.Application.Result;

namespace WorkEnv.Application.CQRS.WorkSpace.Command.Create;

public record CreateCommand(
    Guid ownerId,
    string masterCode
) : IRequest<Result<WorkSpaceDTO>>;