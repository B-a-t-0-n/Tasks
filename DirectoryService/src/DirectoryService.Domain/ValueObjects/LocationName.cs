using CSharpFunctionalExtensions;
using DirectoryService.Domain.Shared;

namespace DirectoryService.Domain.ValueObjects;

public class LocationName : ValueObject
{
    public const int MAX_HIGHT_NAME_LENGTH = 120;
    public const int MAX_LOW_NAME_LENGTH = 3;

    private LocationName() { }
    private LocationName(string value)
    {
        Value = value;
    }

    public string Value { get; } = default!;

    public static Result<LocationName, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Errors.General.ValueIsInvalid("location name");

        if (value.Length < MAX_LOW_NAME_LENGTH || value.Length > MAX_HIGHT_NAME_LENGTH)
            return Errors.General.ValueIsRequired("location name");

        var name = new LocationName(value);

        return name;
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }
}
