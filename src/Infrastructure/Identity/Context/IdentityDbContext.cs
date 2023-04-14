using Infrastructure.Identity.Configurations;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity.Context;

public class IdentityDbContext : IdentityDbContext<ApplicationUser>
{
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
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