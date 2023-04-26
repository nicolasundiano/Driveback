using Application.Users.Common.Models;
using Domain.Users.Entities;
using ErrorOr;

namespace Application.Users.Common.Interfaces;

public interface IUserService
{
    Task<ErrorOr<UserResponse>> UpdateDetails(
        string? firstName,
        string? lastName,
        string? phone,
        Guid userId = default,
        CancellationToken cancellationToken = default);

    Task<ErrorOr<UserResponse>> AddChildUser(
        ChildUser childUser,
        Guid userId = default,
        CancellationToken cancellationToken = default);

    Task<ErrorOr<UserResponse>> GetUserResponse(
        Guid userId = default,
        CancellationToken cancellationToken = default);
}