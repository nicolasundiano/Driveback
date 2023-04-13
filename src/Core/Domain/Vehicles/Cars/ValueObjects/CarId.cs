using Domain.Common;

namespace Domain.Vehicles.Cars.ValueObjects;

public class CarId : ValueObject, IId
{
    public Guid Value { get; }

    private CarId(Guid value)
    {
        Value = value;
    }

    public static CarId Create(Guid value)
    {
        return new CarId(value);
    }

    public static CarId CreateUnique()
    {
        return new CarId(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}