namespace Application.Users.Common.Models;

public record UserResponse(
    string Id,
    string Email,
    string FirstName,
    string LastName,
    string Phone,
    List<ChildUserResponse> ChildUsers);