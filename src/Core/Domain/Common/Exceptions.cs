using System.Text.RegularExpressions;

namespace Domain.Common;

public static class Exceptions
{
    public static void ValidateString(
        string input,
        string parameterName,
        int? maxLength,
        string? regexPattern,
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

    public static void ValidateNotNull<TValue>(
        TValue? value,
        string parameterName)
    {
        if (value is null)
        {
            throw new ArgumentNullException(
                paramName: parameterName,
                message: $"{parameterName} cannot be null.");
        }
    }

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

    public static void ValidateUniqueListProperty<TValue, TKey>(
        IEnumerable<TValue> list,
        string listName,
        Func<TValue, TKey> keySelector,
        string propertyName)
    {
        if (list.GroupBy(keySelector).Any(grouping => grouping.Skip(1).Any()))
        {
            throw new ArgumentException(
                paramName: listName,
                message: $"{propertyName} must be unique in {listName}.");
        }
    }
}