using CSharpFunctionalExtensions;
using DirectoryService.Domain.Shared;

namespace DirectoryService.Domain.ValueObjects;

public class Depth : ValueObject
{
    private Depth() { }
    private Depth(short value)
    {
        Value = value;
    }

    public short Value { get; } = default!;

    public static Result<Depth, Error> Create(short value)
    {
        if (value < 0)
            return Errors.General.ValueIsInvalid("depth");

        var depth = new Depth(value);

        return depth;
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }
}
