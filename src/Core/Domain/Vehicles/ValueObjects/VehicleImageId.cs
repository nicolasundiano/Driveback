using Domain.Common;

namespace Domain.Vehicles.ValueObjects;

public class VehicleImageId : ValueObject
{
    public Guid Value { get; }

    private VehicleImageId(Guid value)
    {
        Value = value;
    }

    public static VehicleImageId Create(Guid value)
    {
        return new VehicleImageId(value);
    }

    public static VehicleImageId CreateUnique()
    {
        return new VehicleImageId(Guid.NewGuid());
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