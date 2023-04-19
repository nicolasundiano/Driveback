namespace Infrastructure.Authentication.Common.Models;

public class AdminUserSettings
{
    public const string SectionName = nameof(AdminUserSettings);
    public string Email { get; init; } = null!;
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
    public string Phone { get; init; } = null!;
    public string Password { get; init; } = null!;
}