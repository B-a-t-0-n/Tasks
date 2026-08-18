namespace DirectoryService.Contracts.Requests.Positions;

public sealed record UpdatePositionRequest(
    string Name,
    string? Description);