using DirectoryService.Domain.Shared;
using DirectoryService.Domain.ValueObjects;
using DirectoryService.Domain.ValueObjects.IDs;

namespace DirectoryService.Domain.Entity;

public sealed class Position : SoftDeletableEntity<PositionId>
{
    private readonly List<DepartmentPosition> _departaments = [];

    public PositionName Name { get; private set; } = default!;

    public Description Description { get; private set; } = default!;

    public IReadOnlyList<DepartmentPosition> Departments => _departaments;


    private Position(PositionId id) : base(id) { }

    public Position(PositionId id, PositionName name, Description description): base(id)
    {
        Name = name;
        Description = description;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}
