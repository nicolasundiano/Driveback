using Application.Common.Interfaces.Persistence;
using Domain.Users;
using Infrastructure.Common.Models;

namespace Infrastructure.Persistence.Initialization;

public static class ApplicationDbInitializer
{
    internal static async Task InitializeDomainAdminUser(
        AdminUserSettings adminUserSettings,
        IRepository<User> userRepository,
        IUnitOfWork unitOfWork)
    {
        var users = await userRepository.ListAsync();
        
        if (users.Count is 0)
        {
            userRepository.Add(
                User.Create(
                    adminUserSettings.Email,
                    adminUserSettings.FirstName,
                    adminUserSettings.LastName,
                    adminUserSettings.Phone));
            
            await unitOfWork.SaveChangesAsync();
        }
    }
}