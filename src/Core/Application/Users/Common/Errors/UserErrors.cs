using ErrorOr;

namespace Application.Users.Common.Errors;

public static class UserErrors
{
    public static Error NotFound => Error.NotFound(
        code: "User.NotFound",
        description: "User not found.");
    
    public static Error AlreadyExists => Error.Conflict(
        code: "User.AlreadyExists",
        description: "User already exists.");
    
    public static Error RegistrationFailed => Error.Failure(
        code: "User.RegistrationFailed",
        description: "User registration failed.");
    
    public static Error InvalidCredentials => Error.Validation(
        code: "User.InvalidCred",
        description: "User invalid credentials.");
    
    public static Error ChildUserProperty2Duplicated => Error.Conflict(
        code: "User.ChildUserProperty2Duplicated",
        description: "User ChildUserProperty2Duplicated.");
}