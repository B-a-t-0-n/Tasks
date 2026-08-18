namespace DirectoryService.Contracts.Response;

public sealed record PositionResponce(
    Guid Id,
    string Name,
    string? Description,
    DateTime CreatedAt,
    DateTime UpdatedAt);