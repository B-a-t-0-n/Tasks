using CSharpFunctionalExtensions;
using DirectoryService.Domain.Shared;

namespace DirectoryService.Domain.ValueObjects;

public class Address : ValueObject
{
    public const int MAX_LENGTH = 500; 
    public const int MAX_PART_LENGTH = 100; 

    public string? Street { get; }
    public string? City { get; }
    public string? PostalCode { get; }
    public string? Region { get; }
    public string Country { get; } 

    private Address(
        string? street,
        string? city,
        string? postalCode,
        string? region,
        string country)
    {
        Street = street?.Trim();
        City = city?.Trim();
        PostalCode = postalCode?.Trim();
        Region = region?.Trim();
        Country = country.Trim();
    }

    public static Result<Address, Error> Create(
        string? street,
        string? city,
        string? postalCode,
        string? region,
        string country)
    {
        if (string.IsNullOrWhiteSpace(country))
            return Errors.General.ValueIsRequired("Country");

        country = country.Trim();
        if (country.Length < 2 || country.Length > MAX_PART_LENGTH)
            return Errors.General.ValueIsRequired("Country");

        if (!string.IsNullOrWhiteSpace(street) && (street.Length < 1 || street.Length > MAX_PART_LENGTH))
            return Errors.General.ValueIsRequired("Street");

        if (!string.IsNullOrWhiteSpace(city) && (city.Length < 1 || city.Length > MAX_PART_LENGTH))
            return Errors.General.ValueIsRequired("City");

        if (!string.IsNullOrWhiteSpace(postalCode) && (postalCode.Length < 1 || postalCode.Length > 20))
            return Errors.General.ValueIsRequired("Postal");

        if (!string.IsNullOrWhiteSpace(region) && (region.Length < 1 || region.Length > MAX_PART_LENGTH))
            return Errors.General.ValueIsRequired("Region");

        var totalLength = (street?.Length ?? 0) +
                          (city?.Length ?? 0) +
                          (postalCode?.Length ?? 0) +
                          (region?.Length ?? 0) +
                          country.Length;

        if (totalLength > MAX_LENGTH)
            return Errors.General.ValueIsInvalid($"address");

        return new Address(street, city, postalCode, region, country);
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Street ?? string.Empty;
        yield return City ?? string.Empty;
        yield return PostalCode ?? string.Empty;
        yield return Region ?? string.Empty;
        yield return Country;
    }
}

