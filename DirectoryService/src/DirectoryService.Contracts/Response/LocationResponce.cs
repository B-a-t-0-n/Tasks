using DirectoryService.Contracts.DTOs;

namespace DirectoryService.Contracts.Response;

public sealed record LocationResponce(
    Guid Id,
    string Name,
    AddressDTO Address,
    string Timezone,
    DateTime CreatedAt,
    DateTime UpdatedAt);
