using DirectoryService.Domain.Shared;
using DirectoryService.Domain.ValueObjects;
using DirectoryService.Domain.ValueObjects.IDs;

namespace DirectoryService.Domain.Entity;

public sealed class Position : Shared.Entity<PositionId>, ISoftDeletableMutable
{
    private readonly List<DepartmentPosition> _departaments = [];

    public PositionName Name { get; private set; } = default!;

    public Description Description { get; private set; } = default!;

    public IReadOnlyList<DepartmentPosition> Departments => _departaments;

    public bool IsDeleted { get; private set; } = false;

    public DateTime? DeletionDate { get; private set; } = null;
    private Position(PositionId id) : base(id) { }

    public Position(PositionId id, PositionName name, Description description): base(id)
    {
        Name = name;
        Description = description;
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
