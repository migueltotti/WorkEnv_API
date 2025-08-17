using System.Net;
using System.Text.Json;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkEnv.API.Response;
/*using WorkEnv.Application.CQRS.WorkSpace.Command.ChangeOwner;
using CreateWorkSpaceCommand = WorkEnv.Application.CQRS.WorkSpace.Command.Create.CreateCommand;
using WorkEnv.Application.CQRS.WorkSpace.Command.Delete;
using WorkEnv.Application.CQRS.WorkSpace.Query.GetAll;
using WorkEnv.Application.CQRS.WorkSpace.Query.GetAllActivitiesByWorkSpaceId;
using WorkEnv.Application.CQRS.WorkSpace.Query.GetAllByUserId;
using WorkEnv.Application.CQRS.WorkSpace.Query.GetById;
using WorkEnv.Application.DTO.Activity;
using WorkEnv.Application.DTO.Task;
using WorkEnv.Application.DTO.WorkSpace;
using WorkEnv.Application.Result;
using CreateEventCommand = WorkEnv.Application.CQRS.Event.Command.Create.CreateCommand;
using CreateTaskCommand = WorkEnv.Application.CQRS.Task.Command.Create.CreateCommand;*/

namespace WorkEnv.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WorkSpacesController : Controller
{
    private readonly ISender _sender;

    public WorkSpacesController(ISender sender)
    {
        _sender = sender;
    }

    /*[HttpGet]
    public async Task<ActionResult<IEnumerable<WorkSpaceDTO>>> GetAll()
    {
        var result = await _sender.Send(new GetAllQuery());
        
        return Ok(result);
    }
    
    [HttpGet("user/{userId:guid}")]
    public async Task<ActionResult<IEnumerable<WorkSpaceDTO>>> GetAllByUserId(Guid userId)
    {
        var result = await _sender.Send(new GetAllByUserIdQuery(userId));
        
        return Ok(result);
    }
    
    [HttpGet("activities/{workSpaceId:guid}")]
    public async Task<ActionResult<List<ActivityDTO>>> GetAllActivitiesByWorkSpaceId(
        Guid workSpaceId,
        [FromQuery] GetAllActivitiesByWorkSpaceIdQuery query)
    {
        if(!workSpaceId.Equals(query.workSpaceId))
            return BadRequest(WorkSpaceErrors.RequestOwnerIdMismatch);
        
        var result = await _sender.Send(query);
        
        return result.IsSuccess ? Ok(result.Value) : result.ToProblemDetails();
    }
    
    [HttpGet("{workSpaceId:guid}")]
    public async Task<ActionResult<WorkSpaceDTO>> GetById(Guid workSpaceId)
    {
        var result = await _sender.Send(new GetByIdQuery(workSpaceId));

        return result.IsSuccess ? Ok(result.Value) : result.ToProblemDetails();
    }
    
    [HttpPost]
    public async Task<ActionResult<WorkSpaceDTO>> CreateWorkSpace([FromBody] CreateWorkSpaceCommand command)
    {
        var result = await _sender.Send(command);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblemDetails();
    }
    
    [HttpPut("{workSpaceId:guid}")]
    public async Task<ActionResult<WorkSpaceDTO>> ChangeOwner(Guid workSpaceId, [FromBody] ChangeOwnerCommand command)
    {
        if(!workSpaceId.Equals(command.workSpaceId))
            return BadRequest(WorkSpaceErrors.RequestOwnerIdMismatch);
        
        var result = await _sender.Send(command);

        return result.IsSuccess ? Ok("WorkSpace Owner changed successfully!") : result.ToProblemDetails();
    }
    
    [HttpPost("event/{workSpaceOwnerId:guid}")]
    public async Task<ActionResult<EventDTO>> CreateEvent(Guid workSpaceOwnerId, [FromBody] CreateEventCommand command)
    {
        if (!workSpaceOwnerId.Equals(command.ownerId))
            return BadRequest(WorkSpaceErrors.RequestOwnerIdMismatch);
        
        var result = await _sender.Send(command);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblemDetails();
    }
    
    [HttpPost("task/{workSpaceOwnerId:guid}")]
    public async Task<ActionResult<TaskDTO>> CreateTask(Guid workSpaceOwnerId, [FromBody] CreateTaskCommand command)
    {
        if (!workSpaceOwnerId.Equals(command.ownerId))
            return BadRequest(WorkSpaceErrors.RequestOwnerIdMismatch);
        
        var result = await _sender.Send(command);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblemDetails();  
    }
    
    [HttpDelete("{workSpaceId:guid}")]
    public async Task<ActionResult<WorkSpaceDTO>> DeleteWorkSpace(Guid workSpaceId, [FromBody] DeleteCommand command)
    {
        if(!workSpaceId.Equals(command.workSpaceId))
            return BadRequest(WorkSpaceErrors.RequestOwnerIdMismatch);
        
        var result = await _sender.Send(command);

        return result.IsSuccess ? Ok("WorkSpace deleted successfully!") : result.ToProblemDetails();
    }*/
}