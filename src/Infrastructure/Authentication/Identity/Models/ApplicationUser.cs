using Domain.Users.ValueObjects;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Authentication.Identity.Models;

public sealed class ApplicationUser : IdentityUser
{
    public UserId DomainUserId { get; }
    
    public ApplicationUser(UserId domainUserId, string email)
    {
        DomainUserId = domainUserId;
        Email = email;
        UserName = email;
    }
}