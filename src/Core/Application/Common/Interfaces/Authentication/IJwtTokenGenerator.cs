using Domain.Common;

namespace Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    Task<string> GenerateToken(IUser user);
}