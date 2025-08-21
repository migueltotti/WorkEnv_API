using System.Net;

namespace WorkEnv.Application.Result;

public class LoginErrors
{
    public static readonly Error IncorrectFormat = new Error(
        "LoginRequestIncorrectFormat", 
        "Login request must be in the correct format.",
        HttpStatusCode.BadRequest
    );
    
    public static readonly Error IncorrectPassword = new Error(
        "LoginRequestIncorrectPassword", 
        "Password incorrect.",
        HttpStatusCode.BadRequest
    );
}