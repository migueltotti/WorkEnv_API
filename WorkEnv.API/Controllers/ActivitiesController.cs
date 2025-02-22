using System.Net;
using System.Text.Json;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkEnv.Application.CQRS.Activity.Command.ChangeAccessOption;
using WorkEnv.Application.CQRS.Activity.Command.ChangeAccessPassword;
using WorkEnv.Application.CQRS.Activity.Command.ChangePrivacy;
using WorkEnv.Application.CQRS.Activity.Command.Delete;
using WorkEnv.Application.CQRS.Activity.Command.SendAdminInvite;
using WorkEnv.Application.CQRS.Activity.Command.SendUserInvite;
using WorkEnv.Application.CQRS.Activity.Command.UpdateStatus;
using WorkEnv.Application.CQRS.Activity.Command.UpgradeMaxNumberOfParticipants;
using WorkEnv.Application.CQRS.Activity.Query.GetAll;
using WorkEnv.Application.CQRS.Activity.Query.GetByName;
using WorkEnv.Application.CQRS.Message.Command.Create;
using WorkEnv.Application.CQRS.Task.Query.GetById;
using WorkEnv.Application.DTO.Activity;
using WorkEnv.Application.DTO.Message;
using WorkEnv.Application.DTO.Task;
using WorkEnv.Application.Result;
using GetEventByIdQuery = WorkEnv.Application.CQRS.Event.Query.GetById.GetByIdQuery;
using GetTaskByIdQuery = WorkEnv.Application.CQRS.Task.Query.GetById.GetByIdQuery;

namespace WorkEnv.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ActivitiesController : Controller
{
    private readonly ISender _sender;

    public ActivitiesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ActivityDTO>>> GetAll()
    {
        return Ok(await _sender.Send(new GetAllQuery()));
    }
    
    [HttpGet("Task/{taskId:guid}")]
    public async Task<ActionResult<TaskDTO>> GetTaskById(Guid taskId)
    {
        var result = await _sender.Send(new GetTaskByIdQuery(taskId));
        
        return result.IsSuccess ? Ok(result.Value) : NotFound(JsonSerializer.SerializeToElement(result.Error));
    }
    
    [HttpGet("Event/{taskId:guid}")]
    public async Task<ActionResult<TaskDTO>> GetEventById(Guid taskId)
    {
        var result = await _sender.Send(new GetEventByIdQuery(taskId));
        
        return result.IsSuccess ? Ok(result.Value) : NotFound(JsonSerializer.SerializeToElement(result.Error));
    }
    
    [HttpGet]
    public async Task<ActionResult<TaskDTO>> GetByName([FromQuery] string name)
    {
        var activities = await _sender.Send(new GetByNameQuery(name));

        return Ok(activities);
    }

    [HttpPost("SendAdminInvite/{activityId:guid}")]
    public async Task<ActionResult> SendAdminInvite(
        Guid activityId,
        [FromBody] SendAdminInviteCommand command)
    {
        if(!command.activityId.Equals(activityId))
            return BadRequest(JsonSerializer.SerializeToElement(ActivityErrors.IdMismatch));
        
        var result = await _sender.Send(command);

        switch (result.IsSuccess)
        {
            case true:
                return Ok($"Activity with id = {activityId} sent Admin Invite successfully!");
            case false:
                if (result.Error.HttpStatusCode == HttpStatusCode.NotFound)
                    return NotFound(JsonSerializer.SerializeToElement(result.Error));
                
                return BadRequest(JsonSerializer.SerializeToElement(result.Error));
        }
    }
    
    [HttpPost("CreateMessage/{activityId:guid}")]
    public async Task<ActionResult<MessageDTO>> CreateMessage(
        Guid activityId,
        [FromBody] CreateCommand command)
    {
        if(!command.activityId.Equals(activityId))
            return BadRequest(JsonSerializer.SerializeToElement(ActivityErrors.IdMismatch));
        
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
    
    [HttpPost("InviteUser/{activityId:guid}")]
    public async Task<ActionResult> InviteUser(Guid activityId, [FromBody] SendUserInviteCommand command)
    {
        if(!command.activityId.Equals(activityId))
            return BadRequest(JsonSerializer.SerializeToElement(ActivityErrors.IdMismatch));

        var result = await _sender.Send(command);

        switch (result.IsSuccess)
        {
            case true:
                return Ok($"User with id = {command.userId} was Invites successfully to Activity with id = {activityId}!");
            case false:
                if (result.Error.HttpStatusCode == HttpStatusCode.NotFound)
                    return NotFound(JsonSerializer.SerializeToElement(result.Error));
                
                return BadRequest(JsonSerializer.SerializeToElement(result.Error));
        }
    }
    
    [HttpPut("ChangeAccessOptions/{activityId:guid}")]
    public async Task<ActionResult> ChangeAccessOptions(
        Guid activityId,
        [FromBody] ChangeAccessOptionsCommand command)
    {
        if(!command.activityId.Equals(activityId))
            return BadRequest(JsonSerializer.SerializeToElement(ActivityErrors.IdMismatch));
        
        var result = await _sender.Send(command);

        switch (result.IsSuccess)
        {
            case true:
                return Ok($"Activity with id = {activityId} changed Access Options successfully!");
            case false:
                if (result.Error.HttpStatusCode == HttpStatusCode.NotFound)
                    return NotFound(JsonSerializer.SerializeToElement(result.Error));
                
                return BadRequest(JsonSerializer.SerializeToElement(result.Error));
        }
    }
    
    [HttpPut("ChangeAccessPassword/{activityId:guid}")]
    public async Task<ActionResult> ChangeAccessPassword(
        Guid activityId,
        [FromBody] ChangeAccessPasswordCommand command)
    {
        if(!command.activityId.Equals(activityId))
            return BadRequest(JsonSerializer.SerializeToElement(ActivityErrors.IdMismatch));
        
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
    
    [HttpPut("ChangePrivacy/{activityId:guid}")]
    public async Task<ActionResult> ChangePrivacy(
        Guid activityId,
        [FromBody] ChangePrivacyCommand command)
    {
        if(!command.activityId.Equals(activityId))
            return BadRequest(JsonSerializer.SerializeToElement(ActivityErrors.IdMismatch));
        
        var result = await _sender.Send(command);

        switch (result.IsSuccess)
        {
            case true:
                return Ok($"Activity with id = {activityId} changed Privacy successfully!");
            case false:
                if (result.Error.HttpStatusCode == HttpStatusCode.NotFound)
                    return NotFound(JsonSerializer.SerializeToElement(result.Error));
                
                return BadRequest(JsonSerializer.SerializeToElement(result.Error));
        }
    }
    
    [HttpPut("UpdateStatus/{activityId:guid}")]
    public async Task<ActionResult> UpdateStatus(
        Guid activityId,
        [FromBody] UpdateStatusCommand command)
    {
        if(!command.activityId.Equals(activityId))
            return BadRequest(JsonSerializer.SerializeToElement(ActivityErrors.IdMismatch));
        
        var result = await _sender.Send(command);

        switch (result.IsSuccess)
        {
            case true:
                return Ok($"Activity with id = {activityId} updated Status successfully!");
            case false:
                if (result.Error.HttpStatusCode == HttpStatusCode.NotFound)
                    return NotFound(JsonSerializer.SerializeToElement(result.Error));
                
                return BadRequest(JsonSerializer.SerializeToElement(result.Error));
        }
    }
    
    [HttpPut("UpgradeMaxNumberOfParticipants/{activityId:guid}")]
    public async Task<ActionResult> UpgradeMaxNumberOfParticipants(
        Guid activityId,
        [FromBody] UpgradeMaxNumberOfParticipantsCommand command)
    {
        if(!command.activityId.Equals(activityId))
            return BadRequest(JsonSerializer.SerializeToElement(ActivityErrors.IdMismatch));
        
        var result = await _sender.Send(command);

        switch (result.IsSuccess)
        {
            case true:
                return Ok($"Activity with id = {activityId} updated Max Number Of Participants successfully!");
            case false:
                if (result.Error.HttpStatusCode == HttpStatusCode.NotFound)
                    return NotFound(JsonSerializer.SerializeToElement(result.Error));
                
                return BadRequest(JsonSerializer.SerializeToElement(result.Error));
        }
    }
    
    [HttpDelete("{activityId:guid}")]
    public async Task<ActionResult> UpdateStatus(Guid activityId, [FromBody] DeleteCommand command)
    {
        if(!command.activityId.Equals(activityId))
            return BadRequest(JsonSerializer.SerializeToElement(ActivityErrors.IdMismatch));

        var result = await _sender.Send(command);

        switch (result.IsSuccess)
        {
            case true:
                return Ok($"Activity with id = {activityId} Deleted successfully!");
            case false:
                if (result.Error.HttpStatusCode == HttpStatusCode.NotFound)
                    return NotFound(JsonSerializer.SerializeToElement(result.Error));
                
                return BadRequest(JsonSerializer.SerializeToElement(result.Error));
        }
    }
}