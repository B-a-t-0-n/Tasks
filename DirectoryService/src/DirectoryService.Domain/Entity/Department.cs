using DirectoryService.Domain.Shared;
using DirectoryService.Domain.ValueObjects;
using DirectoryService.Domain.ValueObjects.IDs;

namespace DirectoryService.Domain.Entity;

public sealed class Department : Shared.Entity<DepartmentId>, ISoftDeletableMutable
{
    private readonly List<DepartmentLocation> _locations = [];

    private readonly List<DepartmentPosition> _positions = [];

    public DepartmentName Name { get; private set; } = default!;

    public Identifier Identifier { get; private set; } = default!;

    public Guid? ParentId { get; private set; }

    public string Path { get; private set; } = default!;

    public Depth Depth { get; private set; } = default!;

    public IReadOnlyList<DepartmentLocation> Locations => _locations;

    public IReadOnlyList<DepartmentPosition> Positions => _positions;

    public bool IsDeleted { get; private set; } = false;

    public DateTime? DeletionDate { get; private set; } = null;
    private Department(DepartmentId id) : base(id) { }

    public Department(
        DepartmentId id,
        DepartmentName name,
        Identifier identifier,
        Guid? parentId, 
        string path,
        Depth depth) : base(id) 
    { 
        Name = name;
        Identifier = identifier;
        ParentId = parentId;
        Path = path;
        Depth = depth;
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
