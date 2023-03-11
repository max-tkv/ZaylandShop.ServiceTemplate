using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZaylandShop.ServiceTemplate.Entities;

namespace ZaylandShop.ServiceTemplate.Storage.Configurations;

public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.ToTable("Users")
            .HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.CreatedAt)
            .HasDefaultValueSql("current_timestamp")
            .ValueGeneratedOnAdd()
            .IsRequired();
        builder.Property(x => x.UpdatedDateTime)
            .HasDefaultValueSql("current_timestamp")
            .ValueGeneratedOnAdd()
            .IsRequired();
    }
}