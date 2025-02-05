using System.Net;

namespace WorkEnv.Application.Result;

public class RoleErrors
{
    public static readonly Error DataIsNull = new Error(
        "RoleDataIsNull", 
        "Role must not be null.",
        HttpStatusCode.BadRequest);
    
    public static readonly Error IncorrectFormatData = new Error(
        "RoleInputNotValid", 
        "Role input is not in the correct format.",
        HttpStatusCode.BadRequest);
    
    public static readonly Error RoleNotFound = new Error(
        "RoleNotFound", 
        "Role with this signature not found.",
        HttpStatusCode.NotFound);
    
    public static readonly Error IdMismatch = new Error(
        "RoleIdMismatch", 
        "Past Id does not match Role id", 
        HttpStatusCode.BadRequest);
    
    public static readonly Error DuplicateData = new Error(
        "RoleDuplicateData", 
        "Past Role with the same name already exists in this Activity", 
        HttpStatusCode.BadRequest);
}