using Domain.Common;
using Domain.Common.Validators;
using Domain.Vehicles.ValueObjects;

namespace Domain.Vehicles.Entities;

public class VehicleImage : Entity<VehicleImageId>
{
    public string Path { get; private set; }

    private VehicleImage(VehicleImageId id, string path) : base(id)
    {
        Path = path;
        
        Validate();
    }

    public static VehicleImage Create(string path)
    {
        return new VehicleImage(VehicleImageId.CreateUnique(), path);
    }

    private void Validate()
    {
        ValidationHelper.ValidateString(Path, nameof(Path), 1000);
    }
}