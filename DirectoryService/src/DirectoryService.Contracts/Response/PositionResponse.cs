namespace DirectoryService.Contracts.Response;

public sealed record PositionResponse(
    Guid Id,
    string Name,
    string? Description,
    DateTime CreatedAt,
    DateTime UpdatedAt);