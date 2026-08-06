using DirectoryService.Domain.Entity;
using DirectoryService.Domain.ValueObjects;
using DirectoryService.Domain.ValueObjects.IDs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DirectoryService.Infrastructure.Postgres.Configurations;

public sealed class PositionConfiguration : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> builder)
    {
        builder.ToTable("positions");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .HasConversion(
                id => id.Value,
                value => PositionId.Create(value))
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
                .HasMaxLength(PositionName.MAX_HIGHT_NAME_LENGTH)
                .IsRequired();
        });

        builder.OwnsOne(x => x.Description, description =>
        {
            description.Property(x => x.Value)
                .HasColumnName("description")
                .HasMaxLength(Description.MAX_HIGHT_NAME_LENGTH);
        });

        builder.HasMany(x => x.Departments)
            .WithOne()
            .HasForeignKey(x => x.PositionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(x => x.Departments)
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.HasIndex(x => x.IsDeleted);
    }
}
