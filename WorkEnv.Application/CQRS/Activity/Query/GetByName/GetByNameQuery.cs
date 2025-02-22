using MediatR;
using WorkEnv.Application.DTO.Activity;

namespace WorkEnv.Application.CQRS.Activity.Query.GetByName;

public record GetByNameQuery(string name) : IRequest<List<ActivityDTO>>;