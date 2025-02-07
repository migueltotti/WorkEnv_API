using MediatR;
using WorkEnv.Application.DTO.Activity;

namespace WorkEnv.Application.CQRS.Activity.Query.GetAll;

public record GetAllQuery() : IRequest<List<ActivityDTO>>;