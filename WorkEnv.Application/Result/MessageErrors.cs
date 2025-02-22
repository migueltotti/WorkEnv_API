using System.Net;

namespace WorkEnv.Application.Result;

public class MessageErrors
{
    public static readonly Error DataIsNull = new Error(
        "MessageDataIsNull", 
        "Message must not be null.",
        HttpStatusCode.BadRequest);
    
    public static readonly Error IncorrectFormatData = new Error(
        "MessageInputNotValid", 
        "Message input is not in the correct format.",
        HttpStatusCode.BadRequest);
    
    public static readonly Error MessageNotFound = new Error(
        "MessageNotFound", 
        "Message with this signature not found.",
        HttpStatusCode.NotFound);
    
    public static readonly Error IdMismatch = new Error(
        "MessageIdMismatch", 
        "Past Id does not match Message id", 
        HttpStatusCode.BadRequest);
}