using System.Net;

namespace WorkEnv.Application.Result;

public class RefreshTokenErrors
{
    public static readonly Error IncorrectTokens = new Error(
        "LoginRequestIncorrectTokens", 
        "Access or Refresh token is invalid.",
        HttpStatusCode.BadRequest
    );
}