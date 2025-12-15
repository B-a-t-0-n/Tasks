using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using DirectoryService.Domain.Shared;

namespace DirectoryService.Domain.ValueObjects;

public class Identifier : ValueObject
{
    public const int MAX_HIGHT_NAME_LENGTH = 150;
    public const int MAX_LOW_NAME_LENGTH = 3;


    private Identifier() { }
    private Identifier(string value)
    {
        Value = value;
    }

    public string Value { get; } = default!;

    public static Result<Identifier, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Errors.General.ValueIsInvalid("identifier");

        if (value.Length < MAX_LOW_NAME_LENGTH || value.Length > MAX_HIGHT_NAME_LENGTH)
            return Errors.General.ValueIsRequired("identifier");

        if (!Regex.IsMatch(value, @"^[A-Za-z]+$"))
            return Errors.General.ValueIsInvalid("identifier");

        var name = new Identifier(value);

        return name;
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }
}