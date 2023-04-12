using System.Text.RegularExpressions;

namespace Domain.Common.Validators;

public static partial class ValidationHelper
{
    public static void ValidateString(
        string input,
        string parameterName,
        int? maxLength,
        bool nullable = false)
    {
        if (!nullable)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentNullException(
                    paramName: parameterName,
                    message: $"{parameterName} cannot be null or empty.");
            }
        }

        if (maxLength is not null)
        {
            if (input.Length > maxLength)
            {
                throw new ArgumentException(
                    paramName: parameterName,
                    message: $"{parameterName} length must be {maxLength} or less.");
            }
        }
    }
    
    public static void ValidateString(
        string input,
        string parameterName,
        int? maxLength,
        string? regexPattern,
        bool nullable = false)
    {
        ValidateString(input, parameterName, maxLength, nullable);

        if (regexPattern is not null)
        {
            var match = Regex.Match(input, regexPattern);

            if (!match.Success || input != match.Value)
            {
                throw new ArgumentException(
                    paramName: parameterName,
                    message: $"{parameterName} doesnt match the regex pattern.");
            }
        }
    }
}