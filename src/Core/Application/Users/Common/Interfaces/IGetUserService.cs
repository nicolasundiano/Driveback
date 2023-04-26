using Application.Users.Common.Models;
using Domain.Users;
using Domain.Users.Entities;
using ErrorOr;

namespace Application.Users.Common.Interfaces;

public interface IGetUserService
{
    Task<User?> GetUser(Guid userId = default, CancellationToken cancellationToken = default);
}