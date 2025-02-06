using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkEnv.Application.CQRS.User.Command.ChangeEmail;
using WorkEnv.Application.CQRS.User.Command.Register;
using WorkEnv.Application.CQRS.User.Query.GetAllQuery;
using WorkEnv.Application.CQRS.User.Query.GetByEmail;
using WorkEnv.Application.CQRS.User.Query.GetById;
using WorkEnv.Application.DTO.User;

namespace WorkEnv.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : Controller
{
    private readonly ISender _sender;

    public UsersController(ISender sender)
    {
        _sender = sender;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<UserDTO>>> GetAll()
    {
        var users = await _sender.Send(new GetAllQuery());

        return Ok(users);
    }

    [HttpGet("{userId:guid}", Name = "GetUserById")]
    public async Task<ActionResult<UserDTO>> GetById(Guid userId)
    {
        var result = await _sender.Send(new GetByIdQuery(userId));
        
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }
    
    [HttpGet("{userEmail}")]
    public async Task<ActionResult<UserDTO>> GetByEmail(string userEmail)
    {
        var result = await _sender.Send(new GetByEmailQuery(userEmail));
        
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }
    
    [HttpGet("{userName:}")]
    public async Task<ActionResult<UserDTO>> GetByName(string userName)
    {
        var result = await _sender.Send(new GetByNameQuery(userName));
        
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }

    [HttpPost]
    public async Task<ActionResult<UserDTO>> RegisterUser([FromBody] RegisterUserCommand command)
    {
        var result = await _sender.Send(command);
        
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }
    
    [HttpPut("{userId:guid}")]
    public async Task<ActionResult<UserDTO>> GetByName(Guid userId, [FromBody] string newEmail)
    {
        var result = await _sender.Send(new ChangeEmailCommand(userId, newEmail));
        
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }
}