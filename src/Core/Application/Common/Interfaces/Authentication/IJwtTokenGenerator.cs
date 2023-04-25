using Domain.Common;

namespace Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(IUser user, IEnumerable<string> roles);
}