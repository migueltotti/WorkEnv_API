using System.Net;
using System.Text.Json;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkEnv.Application.CQRS.WorkSpace.Command.ChangeOwner;
using CreateWorkSpaceCommand = WorkEnv.Application.CQRS.WorkSpace.Command.Create.CreateCommand;
using WorkEnv.Application.CQRS.WorkSpace.Command.Delete;
using WorkEnv.Application.CQRS.WorkSpace.Query.GetAll;
using WorkEnv.Application.CQRS.WorkSpace.Query.GetAllActivitiesByWorkSpaceId;
using WorkEnv.Application.CQRS.WorkSpace.Query.GetAllByUserId;
using WorkEnv.Application.CQRS.WorkSpace.Query.GetById;
using WorkEnv.Application.DTO.Task;
using WorkEnv.Application.DTO.WorkSpace;
using WorkEnv.Application.Result;
using CreateEventCommand = WorkEnv.Application.CQRS.Event.Command.Create.CreateCommand;
using CreateTaskCommand = WorkEnv.Application.CQRS.Task.Command.Create.CreateCommand;

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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<WorkSpaceDTO>>> GetAll()
    {
        var result = await _sender.Send(new GetAllQuery());
        
        return Ok(result);
    }
    
    [HttpGet("User/{userId:guid}")]
    public async Task<ActionResult<IEnumerable<WorkSpaceDTO>>> GetAllByUserId(Guid userId)
    {
        var result = await _sender.Send(new GetAllByUserIdQuery(userId));
        
        return Ok(result);
    }
    
    [HttpGet("Activities/{workSpaceId:guid}")]
    public async Task<ActionResult<IEnumerable<WorkSpaceDTO>>> GetAllActivitiesByWorkSpaceId(
        Guid workSpaceId,
        [FromQuery] GetAllActivitiesByWorkSpaceIdQuery query)
    {
        if(!workSpaceId.Equals(query.workSpaceId))
            return BadRequest(WorkSpaceErrors.RequestOwnerIdMismatch);
        
        var result = await _sender.Send(query);
        
        switch (result.IsSuccess)
        {
            case true:
                return Ok(result.Value);
            case false:
                if (result.Error.HttpStatusCode == HttpStatusCode.NotFound)
                    return NotFound(JsonSerializer.SerializeToElement(result.Error));
                
                return BadRequest(JsonSerializer.SerializeToElement(result.Error));
        }
    }
    
    [HttpGet("/{workSpaceId:guid}")]
    public async Task<ActionResult<WorkSpaceDTO>> GetById(Guid workSpaceId)
    {
        var result = await _sender.Send(new GetByIdQuery(workSpaceId));

        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }
    
    [HttpPost]
    public async Task<ActionResult<WorkSpaceDTO>> CreateWorkSpace([FromBody] CreateWorkSpaceCommand command)
    {
        var result = await _sender.Send(command);

        switch (result.IsSuccess)
        {
            case true:
                return Ok(result.Value);
            case false:
                if (result.Error.HttpStatusCode == HttpStatusCode.NotFound)
                    return NotFound(JsonSerializer.SerializeToElement(result.Error));
                
                return BadRequest(JsonSerializer.SerializeToElement(result.Error));
        }
    }
    
    [HttpPut("/{workSpaceId:guid}")]
    public async Task<ActionResult<WorkSpaceDTO>> ChangeOwner(Guid workSpaceId, [FromBody] ChangeOwnerCommand command)
    {
        if(!workSpaceId.Equals(command.workSpaceId))
            return BadRequest(WorkSpaceErrors.RequestOwnerIdMismatch);
        
        var result = await _sender.Send(command);

        switch (result.IsSuccess)
        {
            case true:
                return Ok("WorkSpace Owner changed successfully!");
            case false:
                if(result.Error.HttpStatusCode == HttpStatusCode.NotFound) 
                    return NotFound(JsonSerializer.SerializeToElement(result.Error));
                
                return BadRequest(JsonSerializer.SerializeToElement(result.Error));
        }      
    }
    
    [HttpPost("/Event/{workSpaceOwnerId:guid}")]
    public async Task<ActionResult<EventDTO>> CreateEvent(Guid workSpaceOwnerId, [FromBody] CreateEventCommand command)
    {
        if (!workSpaceOwnerId.Equals(command.ownerId))
            return BadRequest(WorkSpaceErrors.RequestOwnerIdMismatch);
        
        var result = await _sender.Send(command);

        switch (result.IsSuccess)
        {
            case true:
                return Ok(result.Value);
            case false:
                if(result.Error.HttpStatusCode == HttpStatusCode.NotFound) 
                    return NotFound(JsonSerializer.SerializeToElement(result.Error));
                
                return BadRequest(JsonSerializer.SerializeToElement(result.Error));
        }     
    }
    
    [HttpPost("/Task/{workSpaceOwnerId:guid}")]
    public async Task<ActionResult<TaskDTO>> CreateTask(Guid workSpaceOwnerId, [FromBody] CreateTaskCommand command)
    {
        if (!workSpaceOwnerId.Equals(command.ownerId))
            return BadRequest(WorkSpaceErrors.RequestOwnerIdMismatch);
        
        var result = await _sender.Send(command);

        switch (result.IsSuccess)
        {
            case true:
                return Ok(result.Value);
            case false:
                if(result.Error.HttpStatusCode == HttpStatusCode.NotFound) 
                    return NotFound(JsonSerializer.SerializeToElement(result.Error));
                
                return BadRequest(JsonSerializer.SerializeToElement(result.Error));
        }     
    }
    
    [HttpDelete("/{workSpaceId:guid}")]
    public async Task<ActionResult<WorkSpaceDTO>> DeleteWorkSpace(Guid workSpaceId, [FromBody] DeleteCommand command)
    {
        if(!workSpaceId.Equals(command.workSpaceId))
            return BadRequest(WorkSpaceErrors.RequestOwnerIdMismatch);
        
        var result = await _sender.Send(command);

        switch (result.IsSuccess)
        {
            case true:
                return Ok("WorkSpace deleted successfully!");
            case false:
                if(result.Error.HttpStatusCode == HttpStatusCode.NotFound) 
                    return NotFound(JsonSerializer.SerializeToElement(result.Error));
                
                return BadRequest(JsonSerializer.SerializeToElement(result.Error));
        } 
    }
}