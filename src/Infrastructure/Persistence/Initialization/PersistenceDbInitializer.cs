using Application.Common.Interfaces.Persistence;
using Domain.Admins;
using Infrastructure.Authentication.Common.Models;

namespace Infrastructure.Persistence.Initialization;

public static class PersistenceDbInitializer
{
    internal static async Task InitializeDomainAdmin(
        AdminUserSettings adminUserSettings,
        IRepository<Admin> adminRepository,
        IUnitOfWork unitOfWork)
    {
        if (await adminRepository.CountAsync() is 0)
        {
            var admin = Admin.Create(adminUserSettings.Email);
            
            adminRepository.Add(admin);

            await unitOfWork.SaveChangesAsync();
        }
    }
}