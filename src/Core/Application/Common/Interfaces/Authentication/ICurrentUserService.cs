namespace Application.Common.Interfaces.Authentication;

public interface ICurrentUserService
{
    bool IsAuthenticated { get; }
    Guid UserId { get; }
}