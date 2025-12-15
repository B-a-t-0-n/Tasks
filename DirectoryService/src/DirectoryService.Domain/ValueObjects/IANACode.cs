using CSharpFunctionalExtensions;
using DirectoryService.Domain.Shared;
using System.Text.RegularExpressions;

namespace DirectoryService.Domain.ValueObjects;

public class IANACode : ValueObject
{
    private const int MIN_LENGTH = 2;
    private const int MAX_LENGTH = 35;
    private static readonly Regex IanaRegex = new(@"^[a-zA-Z]{2,3}(-[a-zA-Z0-9]{2,8})*$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    private IANACode() { }
    private IANACode(string value) => Value = value;

    public string Value { get; } = default!;

    public static Result<IANACode, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Errors.General.ValueIsInvalid("IANA code");

        var trimmed = value.Trim();

        if (trimmed.Length < MIN_LENGTH || trimmed.Length > MAX_LENGTH)
            return Errors.General.ValueIsInvalid("IANA code");

        if (!IanaRegex.IsMatch(trimmed))
            return Errors.General.ValueIsInvalid("IANA code");

        var parts = trimmed.Split('-');
        var normalizedParts = new List<string>
        {
            parts[0].ToLowerInvariant()
        };
        normalizedParts.AddRange(parts.Skip(1).Select(p => p.ToUpperInvariant()));

        var normalized = string.Join("-", normalizedParts);

        return new IANACode(normalized);
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }
}