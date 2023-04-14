using Domain.Users;
using Domain.Vehicles;
using Domain.Vehicles.Entities;
using Infrastructure.Persistence.Configurations.Users;
using Infrastructure.Persistence.Configurations.Vehicles;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Context;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    
    public DbSet<Vehicle> Vehicles { get; set; } = null!;
    
    public DbSet<VehicleImage> VehicleImages { get; set; } = null!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new VehicleConfiguration());
        builder.ApplyConfiguration(new VehicleImageConfiguration());
    }
}