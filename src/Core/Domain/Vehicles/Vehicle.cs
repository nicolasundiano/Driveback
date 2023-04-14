using Domain.Common;
using Domain.Common.Validators;
using Domain.Users.ValueObjects;
using Domain.Vehicles.Entities;
using Domain.Vehicles.ValueObjects;

namespace Domain.Vehicles;

public class Vehicle : Entity<VehicleId>, IAggregateRoot
{
    private readonly List<Image> _images = new();
    public UserId UserId { get; private set; }
    public string LicensePlate { get; private set; }
    public IReadOnlyList<Image> Images => _images.AsReadOnly();

    private Vehicle(VehicleId id, UserId userId, string licensePlate, List<Image> images) : base(id)
    {
        UserId = userId;
        LicensePlate = licensePlate;
        _images = images;
        
        Validate();
    }

    private void Validate()
    {
        ValidationHelper.ValidateString(LicensePlate, nameof(LicensePlate), 10);
    }
    
#pragma warning disable CS8618
    private Vehicle()
    {
    }
#pragma warning restore CS8618
}