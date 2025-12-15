using CSharpFunctionalExtensions;

namespace DirectoryService.Domain.ValueObjects.IDs;

public class LocationId : ComparableValueObject
{
    private LocationId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static LocationId NewId() => new(Guid.NewGuid());

    public static LocationId Empty() => new(Guid.Empty);

    public static LocationId Create(Guid id) => new(id);

    public static implicit operator Guid(LocationId id) => id.Value;

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }
}
