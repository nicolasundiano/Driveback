using Application.Common.Interfaces.Persistence;
using Domain.Users;
using Infrastructure.Common.Models;
using Infrastructure.Persistence.Initialization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure.Persistence;

public static class PersistenceInitializer
{
    internal static async Task InitializeDomainAdminUser(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        var adminUserSettings = scope.ServiceProvider.GetRequiredService<IOptions<AdminUserSettings>>();

        var userRepository = scope.ServiceProvider.GetRequiredService<IRepository<User>>();
        
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        await ApplicationDbInitializer.InitializeDomainAdminUser(adminUserSettings.Value, userRepository, unitOfWork);
    }
}