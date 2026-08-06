using DirectoryService.Domain.Entity;
using DirectoryService.Domain.ValueObjects.IDs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DirectoryService.Infrastructure.Postgres.Configurations;

public sealed class DepartmentPositionConfiguration : IEntityTypeConfiguration<DepartmentPosition>
{
    public void Configure(EntityTypeBuilder<DepartmentPosition> builder)
    {
        builder.ToTable("department_positions");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedNever();

        builder.Property(x => x.DepartmentId)
            .HasColumnName("department_id")
            .HasConversion(
                id => id.Value,
                value => DepartmentId.Create(value))
            .ValueGeneratedNever();

        builder.Property(x => x.PositionId)
            .HasColumnName("position_id")
            .HasConversion(
                id => id.Value,
                value => PositionId.Create(value))
            .ValueGeneratedNever();

        builder.HasIndex(x => new { x.DepartmentId, x.PositionId })
            .IsUnique();

        builder.HasOne<Department>()
            .WithMany(x => x.Positions)
            .HasForeignKey(x => x.DepartmentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Position>()
            .WithMany(x => x.Departments)
            .HasForeignKey(x => x.PositionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
