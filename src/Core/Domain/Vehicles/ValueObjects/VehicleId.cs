using Domain.Common;

namespace Domain.Vehicles.ValueObjects;

public class VehicleId : ValueObject
{
    public Guid Value { get; }

    private VehicleId(Guid value)
    {
        Value = value;
    }

    public static VehicleId Create(Guid value)
    {
        return new VehicleId(value);
    }

    public static VehicleId CreateUnique()
    {
        return new VehicleId(Guid.NewGuid());
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