namespace Domain.Common;

public interface IUser
{
    Guid Id { get; }
    string Email { get; }
}