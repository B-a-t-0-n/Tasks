using DirectoryService.Domain.Shared;
using DirectoryService.Domain.ValueObjects;
using DirectoryService.Domain.ValueObjects.IDs;

namespace DirectoryService.Domain.Entity;

public sealed class Location : SoftDeletableEntity<LocationId>
{
    private readonly List<DepartmentLocation> _departaments = [];

    public LocationName Name { get; private set; } = default!;

    public Address Address { get; private set; } = default!;

    public IANACode Timezone { get; private set; } = default!;

    public IReadOnlyList<DepartmentLocation> Departments => _departaments;

    private Location(LocationId id) : base(id) { }

    public Location(LocationId id, Address address, IANACode timezone) : base(id)
    {
        Address = address;
        Timezone = timezone;

        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}
