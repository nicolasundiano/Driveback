using ErrorOr;

namespace Application.Users.Common.Errors;

public static class ChildUserErrors
{
    public static Error NotFound => Error.NotFound(
        code: "ChildUser.NotFound",
        description: "ChildUser not found.");
}