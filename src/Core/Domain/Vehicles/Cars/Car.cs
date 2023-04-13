using Domain.Common;
using Domain.Common.Validators;
using Domain.Vehicles.Cars.ValueObjects;
using Domain.Vehicles.Common;

namespace Domain.Vehicles.Cars;

public class Car : Vehicle
{
    private Car(CarId id, string licensePlate) : base(id, licensePlate)
    {
        Validate();
    }

    public static Car Create(string licensePlate)
    {
        return new Car(CarId.CreateUnique(), licensePlate);
    }

    private new void Validate()
    {
        base.Validate();
        ValidationHelper.ValidateString(LicensePlate, nameof(LicensePlate), 10);
    }
    
#pragma warning disable CS8618
    private Car()
    {
    }
#pragma warning restore CS8618
}