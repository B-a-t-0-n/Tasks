using CSharpFunctionalExtensions;

namespace DirectoryService.Domain.ValueObjects.IDs;

public class DepartmentId : ComparableValueObject
{
    private DepartmentId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static DepartmentId NewId() => new(Guid.NewGuid());

    public static DepartmentId Empty() => new(Guid.Empty);

    public static DepartmentId Create(Guid id) => new(id);

    public static implicit operator Guid(DepartmentId id) => id.Value;

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }
}
