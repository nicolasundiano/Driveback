namespace Domain.Common.Validators;

public static partial class ValidationHelper
{
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
}