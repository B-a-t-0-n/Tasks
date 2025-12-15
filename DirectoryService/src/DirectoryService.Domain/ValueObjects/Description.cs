using CSharpFunctionalExtensions;
using DirectoryService.Domain.Shared;

namespace DirectoryService.Domain.ValueObjects;

public class Description : ValueObject
{
    public const int MAX_HIGHT_NAME_LENGTH = 1000;

    private Description() { }
    private Description(string? value)
    {
        Value = value;
    }

    public string? Value { get; } = default!;

    public static Result<Description, Error> Create(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return new Description(value);

        if (value.Length >= MAX_HIGHT_NAME_LENGTH)
            return Errors.General.ValueIsRequired("");

        var description = new Description(value);

        return description;
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value ?? "";
    }
}

