using DirectoryService.Domain.Entity;
using DirectoryService.Domain.ValueObjects.IDs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DirectoryService.Infrastructure.Postgres.Configurations;

public sealed class DepartmentLocationConfiguration : IEntityTypeConfiguration<DepartmentLocation>
{
    public void Configure(EntityTypeBuilder<DepartmentLocation> builder)
    {
        builder.ToTable("department_locations");

        builder.Ignore(x => x.Id);

        builder.HasKey(x => new { x.DepartmentId, x.LocationId });

        builder.Property(x => x.DepartmentId)
            .HasColumnName("department_id")
            .HasConversion(
                id => id.Value,
                value => DepartmentId.Create(value))
            .ValueGeneratedNever();

        builder.Property(x => x.LocationId)
            .HasColumnName("location_id")
            .HasConversion(
                id => id.Value,
                value => LocationId.Create(value))
            .ValueGeneratedNever();

        builder.HasOne<Department>()
            .WithMany(x => x.Locations)
            .HasForeignKey(x => x.DepartmentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Location>()
            .WithMany(x => x.Departments)
            .HasForeignKey(x => x.LocationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
