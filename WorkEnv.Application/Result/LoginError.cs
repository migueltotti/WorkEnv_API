using System.Net;

namespace WorkEnv.Application.Result;

public class LoginError
{
    public static readonly Error PasswordIncorrect = new Error(
        "LoginPasswordIncorrect", 
        "Password incorrect.",
        HttpStatusCode.BadRequest);
    
    public static readonly Error RefreshTokenInvalid = new Error(
        "UserRefreshTokenInvalid", 
        "RefreshToken invalid.",
        HttpStatusCode.BadRequest);
}