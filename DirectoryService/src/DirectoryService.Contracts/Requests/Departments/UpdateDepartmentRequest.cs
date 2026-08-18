namespace DirectoryService.Contracts.Requests.Departments;

public sealed record UpdateDepartmentRequest(
    string Name,
    string Identifier,
    Guid? ParentId,
    string Path,
    short Depth);