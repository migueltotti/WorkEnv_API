using MediatR;
using WorkEnv.Application.DTO.User;

namespace WorkEnv.Application.CQRS.User.Query.GetAllQuery;

public record GetAllQuery : IRequest<List<UserDTO>>;