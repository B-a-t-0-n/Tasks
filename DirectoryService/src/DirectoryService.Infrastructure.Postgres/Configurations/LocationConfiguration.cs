using DirectoryService.Domain.Entity;
using DirectoryService.Domain.ValueObjects;
using DirectoryService.Domain.ValueObjects.IDs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DirectoryService.Infrastructure.Postgres.Configurations;

public sealed class LocationConfiguration : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.ToTable("locations");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .HasConversion(
                id => id.Value,
                value => LocationId.Create(value))
            .ValueGeneratedNever();

        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .HasColumnName("updated_at")
            .IsRequired();

        builder.Property(x => x.IsDeleted)
            .HasColumnName("is_deleted")
            .IsRequired();

        builder.Property(x => x.DeletionDate)
            .HasColumnName("deletion_date");

        builder.OwnsOne(x => x.Name, name =>
        {
            name.Property(x => x.Value)
                .HasColumnName("name")
                .HasMaxLength(LocationName.MAX_HIGHT_NAME_LENGTH)
                .IsRequired();
        });

        builder.OwnsOne(x => x.Timezone, timezone =>
        {
            timezone.Property(x => x.Value)
                .HasColumnName("timezone")
                .HasMaxLength(35)
                .IsRequired();
        });

        builder.OwnsOne(x => x.Address, address =>
        {
            address.Property(x => x.Street)
                .HasColumnName("street")
                .HasMaxLength(Address.MAX_PART_LENGTH);

            address.Property(x => x.City)
                .HasColumnName("city")
                .HasMaxLength(Address.MAX_PART_LENGTH);

            address.Property(x => x.PostalCode)
                .HasColumnName("postal_code")
                .HasMaxLength(20);

            address.Property(x => x.Region)
                .HasColumnName("region")
                .HasMaxLength(Address.MAX_PART_LENGTH);

            address.Property(x => x.Country)
                .HasColumnName("country")
                .HasMaxLength(Address.MAX_PART_LENGTH)
                .IsRequired();
        });

        builder.Navigation(x => x.Departments)
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.HasIndex(x => x.IsDeleted);
    }
}
