namespace Domain.Common.Validators;

public partial class ValidationHelper
{
    public static void ValidateOutOfRange(
        int input,
        string parameterName,
        int minValue,
        int maxValue)
    {
        if (input < minValue || input > maxValue)
        {
            throw new ArgumentException(
                paramName: parameterName,
                message: $"{parameterName} is out of range ({minValue}-{maxValue})");
        }
    }
}