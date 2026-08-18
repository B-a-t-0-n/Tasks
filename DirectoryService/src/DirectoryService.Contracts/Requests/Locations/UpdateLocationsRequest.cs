using DirectoryService.Contracts.DTOs;

namespace DirectoryService.Contracts.Requests.Locations;

public sealed record UpdateLocationRequest(
    string Name,
    AddressDTO Address,
    string Timezone);