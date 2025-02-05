using System.Net;

namespace WorkEnv.Application.Result;

public class WorkSpaceErrors
{
    public static readonly Error DataIsNull = new Error(
        "WorkSpaceDataIsNull", 
        "WorkSpace must not be null.",
        HttpStatusCode.BadRequest);
    
    public static readonly Error IncorrectFormatData = new Error(
        "WorkSpaceInputNotValid", 
        "WorkSpace input is not in the correct format.",
        HttpStatusCode.BadRequest);
    
    public static readonly Error WorkSpaceNotFound = new Error(
        "WorkSpaceNotFound", 
        "WorkSpace with this signature not found.",
        HttpStatusCode.NotFound);
    
    public static readonly Error IdMismatch = new Error(
        "WorkSpaceIdMismatch", 
        "Past Id does not match WorkSpace id", 
        HttpStatusCode.BadRequest);
}