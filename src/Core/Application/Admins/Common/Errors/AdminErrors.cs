using ErrorOr;

namespace Application.Admins.Common.Errors;

public static class AdminErrors
{
    public static Error AlreadyExists => Error.Conflict(
        code: "Admin.AlreadyExists",
        description: "Admin already exists.");
    
    public static Error RegistrationFailed => Error.Failure(
        code: "Admin.RegistrationFailed",
        description: "Admin registration failed.");
    
    public static Error InvalidCredentials => Error.Validation(
        code: "Admin.InvalidCred",
        description: "Admin invalid credentials.");
}