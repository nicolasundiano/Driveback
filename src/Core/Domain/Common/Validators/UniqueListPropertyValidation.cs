namespace Domain.Common.Validators;

public static partial class Throw
{
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