using Domain.Common.Validators;
using Domain.Users.Entities.Base;
using Domain.Users.ValueObjects;

namespace Domain.Users.Entities;

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

    private void Validate()
    {
        ValidationHelper.ValidateString(LicensePlate, nameof(LicensePlate), 10);
    }
    
#pragma warning disable CS8618
    private Car()
    {
    }
#pragma warning restore CS8618
}