using Domain.Users.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Users;

public class ChildUserConfiguration : IEntityTypeConfiguration<ChildUser>
{
    public void Configure(EntityTypeBuilder<ChildUser> builder)
    {
        builder.ToTable("ChildUsers");

        builder.HasKey(cu => cu.Id);
        
        builder.Property(cu => cu.Id)
            .ValueGeneratedNever();
        
        builder.Property(cu => cu.Property1)
            .HasMaxLength(100);
    }
}