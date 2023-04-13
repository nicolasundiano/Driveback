using Domain.Common;
using Domain.Common.Validators;

namespace Domain.Vehicles.Common;

public abstract class Vehicle : Entity<IId>, IAggregateRoot
{
    public string LicensePlate { get; private set; }

    protected Vehicle(IId id, string licensePlate) : base(id)
    {
        LicensePlate = licensePlate;
    }

    protected void Validate()
    {
        ValidationHelper.ValidateString(LicensePlate, nameof(LicensePlate), 10);
    }
    
#pragma warning disable CS8618
    protected Vehicle()
    {
    }
#pragma warning restore CS8618
}