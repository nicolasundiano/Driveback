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
        
        builder.Property(c => c.Property1)
            .HasMaxLength(100);
    }
}