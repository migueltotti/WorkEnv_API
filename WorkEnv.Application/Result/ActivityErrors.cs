using System.Net;

namespace WorkEnv.Application.Result;

public class ActivityErrors
{
    public static readonly Error DataIsNull = new Error(
        "ActivityDataIsNull", 
        "Activity must not be null.",
        HttpStatusCode.BadRequest);
    
    public static readonly Error IncorrectFormatData = new Error(
        "ActivityInputNotValid", 
        "Activity input is not in the correct format.",
        HttpStatusCode.BadRequest);
    
    public static readonly Error ActivityNotFound = new Error(
        "ActivityNotFound", 
        "Activity with this signature not found.",
        HttpStatusCode.NotFound);
    
    public static readonly Error IdMismatch = new Error(
        "ActivityIdMismatch", 
        "Past Id does not match Activity id", 
        HttpStatusCode.BadRequest);
}