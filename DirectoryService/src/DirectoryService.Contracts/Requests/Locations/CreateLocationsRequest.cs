using DirectoryService.Contracts.DTOs;

namespace DirectoryService.Contracts.Requests.Locations;

public sealed record CreateLocationRequest(
    string Name,
    AddressDTO Address,
    string Timezone);
