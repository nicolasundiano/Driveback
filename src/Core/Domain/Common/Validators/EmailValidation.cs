using System.Text.RegularExpressions;

namespace Domain.Common.Validators;

public static partial class ValidationHelper
{
    public static void ValidateEmail(
        string email,
        string parameterName)
    {
        if (string.IsNullOrEmpty(email))
        {
            throw new ArgumentNullException(
                paramName: parameterName,
                message: $"{parameterName} cannot be null or empty.");
        }
        
        if (email.Length > 320)
        {
            throw new ArgumentException(
                paramName: parameterName,
                message: $"{parameterName} length must be 320 or less.");
        }
        
        var match = Regex.Match(email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");

        if (!match.Success || email != match.Value)
        {
            throw new ArgumentException(
                paramName: parameterName,
                message: $"{parameterName} is not a valid email.");
        }
    }
}