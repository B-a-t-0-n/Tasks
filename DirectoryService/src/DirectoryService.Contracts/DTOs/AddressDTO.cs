namespace DirectoryService.Contracts.DTOs;

public sealed record AddressDTO(
    string? Street,
    string? City,
    string? PostalCode,
    string? Region,
    string Country);
