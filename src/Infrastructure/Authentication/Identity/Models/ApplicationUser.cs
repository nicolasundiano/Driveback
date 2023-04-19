using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Authentication.Identity.Models;

public sealed class ApplicationUser : IdentityUser
{
    public Guid DomainUserId { get; init; }
    
    public ApplicationUser(Guid domainUserId, string email)
    {
        DomainUserId = domainUserId;
        Email = email;
        UserName = email;
    }
}