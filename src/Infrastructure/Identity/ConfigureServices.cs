using Application.Users.Common.Interfaces;
using Infrastructure.Identity.Models;
using Infrastructure.Identity.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Identity;

internal static class ConfigureServices
{
    internal static IServiceCollection ConfigureIdentity(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddDbContext<IdentityDbContext>(c =>
            c.UseSqlServer(configuration.GetConnectionString("IdentityConnection")));
        
        services.AddScoped<IIdentityRepository, IdentityRepository>();
        
        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<IdentityDbContext>()
            .AddSignInManager<SignInManager<ApplicationUser>>()
            .AddDefaultTokenProviders();
        
        return services;
    }
}