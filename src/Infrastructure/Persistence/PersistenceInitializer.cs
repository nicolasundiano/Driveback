using Application.Common.Interfaces.Persistence;
using Domain.Admins;
using Infrastructure.Authentication.Common.Models;
using Infrastructure.Persistence.Initialization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure.Persistence;

public static class PersistenceInitializer
{
    internal static async Task InitializeDomainAdmin(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        var adminUserSettings = scope.ServiceProvider.GetRequiredService<IOptions<AdminUserSettings>>();
        var adminRepository = scope.ServiceProvider.GetRequiredService<IRepository<Admin>>();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        await PersistenceDbInitializer.InitializeDomainAdmin(adminUserSettings.Value, adminRepository, unitOfWork);
    }
}