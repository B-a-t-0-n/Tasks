using CSharpFunctionalExtensions;

namespace DirectoryService.Domain.ValueObjects.IDs;

public class PositionId : ComparableValueObject
{
    private PositionId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static PositionId NewAdminId() => new(Guid.NewGuid());

    public static PositionId Empty() => new(Guid.Empty);

    public static PositionId Create(Guid id) => new(id);

    public static implicit operator Guid(PositionId id) => id.Value;

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }
}