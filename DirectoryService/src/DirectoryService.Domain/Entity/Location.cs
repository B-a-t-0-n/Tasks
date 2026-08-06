using DirectoryService.Domain.Shared;
using DirectoryService.Domain.ValueObjects;
using DirectoryService.Domain.ValueObjects.IDs;

namespace DirectoryService.Domain.Entity;

public sealed class Location : Shared.Entity<LocationId>, ISoftDeletableMutable
{
    private readonly List<DepartmentLocation> _departments = [];

    public LocationName Name { get; private set; } = default!;

    public Address Address { get; private set; } = default!;

    public IANACode Timezone { get; private set; } = default!;

    public IReadOnlyList<DepartmentLocation> Departments => _departments;

    public bool IsDeleted { get; private set; } = false;

    public DateTime? DeletionDate { get; private set; } = null;

    private Location(LocationId id) : base(id) { }

    public Location(LocationId id, LocationName name, Address address, IANACode timezone) : base(id)
    {
        Address = address;
        Timezone = timezone;
        Name = name;

        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkAsDeleted()
    {
        if (!IsDeleted)
        {
            IsDeleted = true;
            DeletionDate = DateTime.UtcNow;
        }
    }
    public void Restore()
    {
        if (IsDeleted)
        {
            IsDeleted = false;
            DeletionDate = null;
        }
    }
}
