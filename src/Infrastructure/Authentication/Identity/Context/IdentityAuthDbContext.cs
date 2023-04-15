using Infrastructure.Authentication.Identity.Configurations;
using Infrastructure.Authentication.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Authentication.Identity.Context;

public class IdentityAuthDbContext : IdentityDbContext<ApplicationUser>
{
    public IdentityAuthDbContext(DbContextOptions<IdentityAuthDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.ApplyConfiguration(new ApplicationUserConfig());
        builder.ApplyConfiguration(new IdentityRoleConfig());
        builder.ApplyConfiguration(new IdentityRoleClaimConfig());
        builder.ApplyConfiguration(new IdentityUserRoleConfig());
        builder.ApplyConfiguration(new IdentityUserClaimConfig());
        builder.ApplyConfiguration(new IdentityUserLoginConfig());
        builder.ApplyConfiguration(new IdentityUserTokenConfig());
    }
}