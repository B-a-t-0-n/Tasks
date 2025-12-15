using CSharpFunctionalExtensions;
using DirectoryService.Domain.Shared;

namespace DirectoryService.Domain.ValueObjects;

public class DepartmentName : ValueObject
{
    public const int MAX_HIGHT_NAME_LENGTH = 150;
    public const int MAX_LOW_NAME_LENGTH = 2;

    private DepartmentName() { }
    private DepartmentName(string value)
    {
        Value = value;
    }

    public string Value { get; } = default!;

    public static Result<DepartmentName, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Errors.General.ValueIsInvalid("department name");

        if (value.Length < MAX_LOW_NAME_LENGTH || value.Length > MAX_HIGHT_NAME_LENGTH)
            return Errors.General.ValueIsRequired("department name");

        var name = new DepartmentName(value);

        return name;
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }
}
