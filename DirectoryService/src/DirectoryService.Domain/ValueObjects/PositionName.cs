using CSharpFunctionalExtensions;
using DirectoryService.Domain.Shared;

namespace DirectoryService.Domain.ValueObjects;

public class PositionName : ValueObject
{
    public const int MAX_HIGHT_NAME_LENGTH = 100;
    public const int MAX_LOW_NAME_LENGTH = 3;

    private PositionName() { }
    private PositionName(string value)
    {
        Value = value;
    }

    public string Value { get; } = default!;

    public static Result<PositionName, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Errors.General.ValueIsInvalid("position name");

        if (value.Length < MAX_LOW_NAME_LENGTH || value.Length > MAX_HIGHT_NAME_LENGTH)
            return Errors.General.ValueIsRequired("position name");

        var name = new PositionName(value);

        return name;
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }
}