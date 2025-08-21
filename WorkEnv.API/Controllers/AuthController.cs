using System.Net;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WorkEnv.API.Response;
using WorkEnv.Application.CQRS.Auth.Login;
using WorkEnv.Application.CQRS.Auth.RefreshToken;
using WorkEnv.Application.DTO.Auth;
using WorkEnv.Application.Result;
using WorkEnv.Infrastructure.Identity;

// using WorkEnv.Application.CQRS.Auth.Login;
// using WorkEnv.Application.CQRS.Auth.RefreshToken;
// using WorkEnv.Application.DTO.Auth;

namespace WorkEnv.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class AuthController : Controller
{
    private readonly ISender _sender;
    private readonly ILogger<AuthController> _logger;
    private readonly UserManager<ApplicationUser> _userManager;

    public AuthController(ISender sender, ILogger<AuthController> logger, UserManager<ApplicationUser> userManager)
    {
        _sender = sender;
        _logger = logger;
        _userManager = userManager;
    }

    [HttpPost("login")]
    public async Task<ActionResult<TokenResponse>> Login([FromBody] LoginCommand request)
    {
        if (request is null)
            return BadRequest("Login model is null");

        var result = await _sender.Send(request);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblemDetails();
    }
    
    [HttpPost("refreshToken")]
    public async Task<ActionResult<TokenResponse>> RefreshToken([FromBody] RefreshTokenCommand request)
    {
        if (request is null)
            return BadRequest("RefreshToken model is null");
        
        var result = await _sender.Send(request);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblemDetails(); 
    }

    [HttpPost("testUserRegister")]
    public async Task<ActionResult<ApplicationUser>> TestGenerateApplicationUser([FromBody] TestApplicationUser newUser)
    {
        var newApplicationUser = new ApplicationUser()
        {
            UserName = newUser.username.Replace(" ", ""),
            Email = newUser.email,
            SecurityStamp = Guid.NewGuid().ToString(),
        };

        var result = await _userManager.CreateAsync(newApplicationUser, newUser.password);
        
        if(!result.Succeeded)
            return BadRequest(result.Errors);
        
        return Ok(newApplicationUser);
    }
}

public record TestApplicationUser(string username, string email, string password);