using System.Net;
using System.Text.Json;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkEnv.API.Response;
using WorkEnv.Application.CQRS.User.Command.Register;
using WorkEnv.Application.CQRS.User.Query.GetAllQuery;
using WorkEnv.Application.CQRS.User.Query.GetByEmail;
using WorkEnv.Application.CQRS.User.Query.GetById;
using WorkEnv.Application.DTO.User;

// using WorkEnv.Application.CQRS.User.Command.ChangeEmail;
// using WorkEnv.Application.CQRS.User.Command.ChangeName;
// using WorkEnv.Application.CQRS.User.Command.ChangePassword;
// using WorkEnv.Application.CQRS.User.Command.Delete;
// using WorkEnv.Application.CQRS.User.Command.Register;
// using WorkEnv.Application.CQRS.User.Query.GetAllQuery;
// using WorkEnv.Application.CQRS.User.Query.GetByEmail;
// using WorkEnv.Application.CQRS.User.Query.GetById;
// using WorkEnv.Application.DTO.User;

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
        
        return result.IsSuccess ? Ok(result.Value) : result.ToProblemDetails();
    }
    
    [HttpGet("email")]
    public async Task<ActionResult<UserDTO>> GetByEmail([FromQuery] string userEmail)
    {
        var result = await _sender.Send(new GetByEmailQuery(userEmail));
        
        return result.IsSuccess ? Ok(result.Value) : result.ToProblemDetails();
    }
    
    [HttpGet("name")]
    public async Task<ActionResult<UserDTO>> GetByName([FromQuery] string userName)
    {
        var result = await _sender.Send(new GetByNameQuery(userName));
        
        return result.IsSuccess ? Ok(result.Value) : result.ToProblemDetails();
    }
    
    [HttpPost]
    public async Task<ActionResult<UserDTO>> RegisterUser([FromBody] RegisterUserCommand command)
    {
        var result = await _sender.Send(command);
        
        return result.IsSuccess ? 
            CreatedAtRoute("GetUserById", new { userId = result.Value.UserId}, result.Value)
            : result.ToProblemDetails();
    }
    
    /*
    [HttpPut("changeName/{userId:guid}")]
    public async Task<ActionResult<UserDTO>> ChangeName(Guid userId, ChangeNameCommand command)
    {
        if(!userId.Equals(command.userId))
            return BadRequest("UserId does not match");

        var result = await _sender.Send(command);

        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }

    [HttpPut("changeEmail/{userId:guid}")]
    public async Task<ActionResult<UserDTO>> ChangeEmail(Guid userId, ChangeEmailCommand command)
    {
        if(!userId.Equals(command.userId))
            return BadRequest("UserId does not match");

        var result = await _sender.Send(command);

        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }

    [HttpPut("changePassword/{userId:guid}")]
    public async Task<ActionResult<UserDTO>> ChangePassword(Guid userId, ChangePasswordCommand command)
    {
        if(!userId.Equals(command.userId))
            return BadRequest("UserId does not match");

        var result = await _sender.Send(command);

        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }

    [HttpDelete("{userId:guid}")]
    public async Task<ActionResult<UserDTO>> DeleteUser(Guid userId)
    {
        var result = await _sender.Send(new DeleteUserCommand(userId));

        return result.IsSuccess ? Ok("User delete successfully!") : result.ToProblemDetails();
    }*/
}