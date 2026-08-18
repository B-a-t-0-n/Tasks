using DirectoryService.Contracts.DTOs;

namespace DirectoryService.Contracts.Response;

public sealed record LocationResponse(
    Guid Id,
    string Name,
    AddressDTO Address,
    string Timezone,
    DateTime CreatedAt,
    DateTime UpdatedAt);
