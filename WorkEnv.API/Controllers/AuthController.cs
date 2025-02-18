using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkEnv.Application.CQRS.Auth.Login;
using WorkEnv.Application.CQRS.Auth.RefreshToken;
using WorkEnv.Application.DTO.Auth;

namespace WorkEnv.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class AuthController : Controller
{
    private readonly ISender _sender;

    public AuthController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("login")]
    public async Task<ActionResult<TokenResponse>> Login([FromBody] LoginCommand request)
    {
        var result = await _sender.Send(request);

        switch (result.IsSuccess)
        {
            case true:
                return Ok(result.Value);
            case false:
                if (result.Error.HttpStatusCode.Equals(HttpStatusCode.NotFound))
                    return NotFound(result.Error);
                
                return BadRequest(result.Error);
        }
    }
    
    [HttpPost("refreshToken")]
    public async Task<ActionResult<TokenResponse>> RefreshToken([FromBody] RefreshTokenCommand request)
    {
        var result = await _sender.Send(request);

        switch (result.IsSuccess)
        {
            case true:
                return Ok(result.Value);
            case false:
                if (result.Error.HttpStatusCode.Equals(HttpStatusCode.NotFound))
                    return NotFound(result.Error);
                
                return BadRequest(result.Error);
        }
    }
}