using DirectoryService.Domain.Entity;
using DirectoryService.Domain.ValueObjects;
using DirectoryService.Domain.ValueObjects.IDs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DirectoryService.Infrastructure.Postgres.Configurations;

public sealed class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable("departments");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .HasConversion(
                id => id.Value,
                value => DepartmentId.Create(value))
            .ValueGeneratedNever();

        builder.Property(x => x.ParentId)
            .HasColumnName("parent_id")
            .HasConversion(
                id => id!.Value,
                value => DepartmentId.Create(value));

        builder.Property(x => x.Path)
            .HasColumnName("path")
            .HasMaxLength(1024)
            .IsRequired();

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
                .HasMaxLength(DepartmentName.MAX_HIGHT_NAME_LENGTH)
                .IsRequired();
        });

        builder.OwnsOne(x => x.Identifier, identifier =>
        {
            identifier.Property(x => x.Value)
                .HasColumnName("identifier")
                .HasMaxLength(Identifier.MAX_HIGHT_NAME_LENGTH)
                .IsRequired();

            identifier.HasIndex(x => x.Value)
                .IsUnique();
        });

        builder.OwnsOne(x => x.Depth, depth =>
        {
            depth.Property(x => x.Value)
                .HasColumnName("depth")
                .IsRequired();
        });

        builder.Navigation(x => x.Locations)
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.Navigation(x => x.Positions)
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.HasOne<Department>()
            .WithMany()
            .HasForeignKey(x => x.ParentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.Path);
        builder.HasIndex(x => x.ParentId);
        builder.HasIndex(x => x.IsDeleted);
    }
}
