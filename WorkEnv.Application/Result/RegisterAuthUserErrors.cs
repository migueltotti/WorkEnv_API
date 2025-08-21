using System.Net;

namespace WorkEnv.Application.Result;

public class RegisterAuthUserErrors
{
    public static readonly Error IncorrectFormat = new Error(
        "RegisterAuthUserRequestIncorrectFormat", 
        "Auth User must be in the correct format.",
        HttpStatusCode.BadRequest
    );
    
    public static readonly Error AuthUserExists = new Error(
        "RegisterAuthUserAlreadyExists", 
        "Auth User with the same credentials already exists.",
        HttpStatusCode.BadRequest
    );
    
    public static readonly Error CreationFailed = new Error(
        "RegisterAuthUserCreationFailed", 
        "Auth User creation failed, try again later.",
        HttpStatusCode.InternalServerError
    );
}