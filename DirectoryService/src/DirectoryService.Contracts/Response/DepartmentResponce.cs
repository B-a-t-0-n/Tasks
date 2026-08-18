namespace DirectoryService.Contracts.Response;

public sealed record DepartmentResponce(
    Guid Id,
    string Name, 
    string Identifier,
    Guid? ParentId,
    string Path,
    short Depth, 
    DateTime CreatedAt, 
    DateTime UpdatedAt);
