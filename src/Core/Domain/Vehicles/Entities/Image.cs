using Domain.Common;
using Domain.Common.Validators;
using Domain.Vehicles.ValueObjects;

namespace Domain.Vehicles.Entities;

public class Image : Entity<ImageId>
{
    public string Path { get; private set; }

    private Image(ImageId id, string path) : base(id)
    {
        Path = path;
        
        Validate();
    }

    public static Image Create(string path)
    {
        return new Image(ImageId.CreateUnique(), path);
    }

    private void Validate()
    {
        ValidationHelper.ValidateString(Path, nameof(Path), 1000);
    }
}