using DirectoryService.Domain.ValueObjects.IDs;

namespace DirectoryService.Domain.Entity;

public sealed class DepartmentPosition
{
    private DepartmentPosition() { }

    public DepartmentPosition(DepartmentId departmentId, PositionId positionId)
    {
        Id = Guid.CreateVersion7();
        DepartmentId = departmentId;
        PositionId = positionId;
    }

    public Guid Id { get; private set; }

    public DepartmentId DepartmentId { get; private set; } = null!;

    public PositionId PositionId { get; private set; } = null!;

}
