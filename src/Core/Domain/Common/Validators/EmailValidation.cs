using System.Text.RegularExpressions;

namespace Domain.Common.Validators;

public static partial class ValidationHelper
{
    public static void ValidateEmail(
        string email,
        string? parameterName = null)
    {
        var name = parameterName ?? nameof(email);
        
        if (string.IsNullOrEmpty(email))
        {
            throw new ArgumentNullException(
                paramName: name,
                message: $"{name} cannot be null or empty.");
        }
        
        if (email.Length > 320)
        {
            throw new ArgumentException(
                paramName: name,
                message: $"{name} length must be 320 or less.");
        }
        
        var match = Regex.Match(email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");

        if (!match.Success || email != match.Value)
        {
            throw new ArgumentException(
                paramName: name,
                message: $"{name} is not a valid email.");
        }
    }
}