using Domain.Common;

namespace Domain.Users.Entities.Base;

public abstract class Vehicle : Entity<IId>
{
    public string LicensePlate { get; private set; }

    protected Vehicle(IId id, string licensePlate) : base(id)
    {
        LicensePlate = licensePlate;
    }
    
#pragma warning disable CS8618
    protected Vehicle()
    {
    }
#pragma warning restore CS8618
}