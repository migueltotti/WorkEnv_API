using System.Net;

namespace WorkEnv.Application.Result;

public static class UserErrors
{
    public static readonly Error DataIsNull = new Error(
        "UserDataIsNull", 
        "User must not be null.",
        HttpStatusCode.BadRequest);
    
    public static readonly Error IncorrectFormatData = new Error(
        "UserInputNotValid", 
        "User input is not in the correct format.",
        HttpStatusCode.BadRequest);
    
    public static readonly Error IncorrectEmailFormat = new Error(
        "UserEmailInputNotValid", 
        "User email input is not in the correct format.",
        HttpStatusCode.BadRequest);
    
    public static readonly Error IncorrectPasswordFormat = new Error(
        "UserEmailInputNotValid", 
        "User password input must be at least 8 characters and maximum 30 characters, contain 1 uppercase letter, one lowercase letter, one number and one special character.",
        HttpStatusCode.BadRequest);
    
    public static readonly Error IncorrectNameFormat = new Error(
        "UserNameInputNotValid", 
        "User name input must be least 5 characters and maximum 80 characters with the first character upper case.",
        HttpStatusCode.BadRequest);
    
    public static Error UserNotFound => new Error(
        "UserNotFound",
        "User with this signature not found.",
        HttpStatusCode.NotFound);
    
    public static readonly Error IdMismatch = new Error(
        "UserIdMismatch", 
        "Past Id does not match User id.", 
        HttpStatusCode.BadRequest);
    
    public static Error EmailExists => new Error(
        "UserEmailExists",
        "User with this email already exists.",
        HttpStatusCode.BadRequest);
    
    public static Error EqualEmail => new Error(
        "UserEmailExists",
        "New email is equal to User email.",
        HttpStatusCode.BadRequest);
    
    public static Error EqualPassword => new Error(
        "UserEqualPassword",
        "New password cannot be equal to older password.",
        HttpStatusCode.BadRequest);
    
    public static Error OldPasswordMismatch => new Error(
        "UserOldPasswordMismatch",
        "Past password does not match user's old password.",
        HttpStatusCode.BadRequest);
    
    public static Error CpfOrCnpjExists => new Error(
        "UserCpfOrCnpjExists",
        "User with the same cpf or cnpj already exists.",
        HttpStatusCode.BadRequest);
    
}