using Application.Common.Interfaces.Persistence;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence;

internal static class Dependencies
{
    internal static IServiceCollection ConfigurePersistence(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped(typeof(IReadRepository<>), typeof(EfReadRepository<>));
        
        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
        
        services.AddDbContext<ApplicationDbContext>(c =>
            c.UseSqlServer(configuration.GetConnectionString("AppConnection")));

        return services;
    }
}