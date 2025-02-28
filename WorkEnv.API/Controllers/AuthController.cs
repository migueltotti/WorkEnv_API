using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkEnv.API.Response;
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

        return result.IsSuccess ? Ok(result.Value) : result.ToProblemDetails(); 
    }
    
    [HttpPost("refreshToken")]
    public async Task<ActionResult<TokenResponse>> RefreshToken([FromBody] RefreshTokenCommand request)
    {
        var result = await _sender.Send(request);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblemDetails(); 
    }
}