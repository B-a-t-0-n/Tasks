using DirectoryService.Domain.ValueObjects.IDs;

namespace DirectoryService.Domain.Entity;

public sealed class DepartmentLocation
{
    private DepartmentLocation() { }

    public DepartmentLocation(DepartmentId departmentId, LocationId locationId)
    {
        Id = Guid.CreateVersion7();
        DepartmentId = departmentId;
        LocationId = locationId;
    }

    public Guid Id { get; private set; }

    public DepartmentId DepartmentId { get; private set; } = null!;

    public LocationId LocationId { get; private set; } = null!;

}
