namespace Application.Users.Authentication.Common.Models;

public record CurrentUserResponse(
    string Id,
    string Email,
    string FirstName,
    string LastName,
    string Phone);