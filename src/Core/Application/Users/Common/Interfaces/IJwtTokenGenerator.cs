using Domain.Users;

namespace Application.Users.Common.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}