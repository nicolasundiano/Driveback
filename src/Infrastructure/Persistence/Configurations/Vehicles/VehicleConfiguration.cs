using Domain.Vehicles;
using Domain.Vehicles.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Vehicles;

internal class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.ToTable("Vehicles");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => VehicleId.Create(value));
        
        builder.Property(c => c.LicensePlate)
            .HasMaxLength(10);
        
        builder.HasMany(c => c.Images)
            .WithOne()
            .HasForeignKey(nameof(VehicleId))
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
        
        builder.Metadata.FindNavigation(nameof(Vehicle.Images))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}