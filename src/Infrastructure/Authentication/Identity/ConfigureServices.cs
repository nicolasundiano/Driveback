using Application.Users.Authentication.Common.Interfaces;
using Infrastructure.Authentication.Identity.Context;
using Infrastructure.Authentication.Identity.Models;
using Infrastructure.Authentication.Identity.Services;
using Microsoft.AspNetCore.Identity;
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
        services.AddDbContext<IdentityAuthDbContext>(c =>
            c.UseSqlServer(configuration.GetConnectionString("IdentityConnection")));
        
        services.AddScoped<IIdentityService, IdentityService>();
        
        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<IdentityAuthDbContext>()
            .AddSignInManager<SignInManager<ApplicationUser>>()
            .AddDefaultTokenProviders();
        
        return services;
    }
}