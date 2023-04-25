using Domain.Common;
using Domain.Users;

namespace Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(IUser user, IEnumerable<string> roles);
}