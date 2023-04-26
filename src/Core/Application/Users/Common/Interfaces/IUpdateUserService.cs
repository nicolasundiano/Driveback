using Application.Users.Common.Models;
using ErrorOr;

namespace Application.Users.Common.Interfaces;

public interface IUpdateUserService
{
    Task<ErrorOr<UserResponse>> UpdateDetails(
        string? firstName,
        string? lastName,
        string? phone,
        Guid userId = default,
        CancellationToken cancellationToken = default);
}