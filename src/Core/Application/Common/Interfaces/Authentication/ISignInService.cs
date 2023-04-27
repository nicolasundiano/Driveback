namespace Application.Common.Interfaces.Authentication;

public interface ISignInService
{
    Task<bool> SignInAsync(string email, string password);
}