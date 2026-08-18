namespace DirectoryService.Contracts.Response;

public sealed record DepartmentResponse(
    Guid Id,
    string Name, 
    string Identifier,
    Guid? ParentId,
    string Path,
    short Depth, 
    DateTime CreatedAt, 
    DateTime UpdatedAt);
