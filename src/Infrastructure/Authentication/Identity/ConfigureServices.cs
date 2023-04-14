using Application.Users.Common.Interfaces;
using Infrastructure.Authentication.Identity.Models;
using Infrastructure.Authentication.Identity.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Authentication.Identity;

internal static class ConfigureServices
{
    internal static IServiceCollection ConfigureIdentity(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddDbContext<IdentityDbContext>(c =>
            c.UseSqlServer(configuration.GetConnectionString("IdentityConnection")));
        
        services.AddScoped<IIdentityService, IdentityService>();
        
        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<IdentityDbContext>()
            .AddSignInManager<SignInManager<ApplicationUser>>()
            .AddDefaultTokenProviders();
        
        return services;
    }
}