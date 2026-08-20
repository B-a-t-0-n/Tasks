namespace DirectoryService.Contracts.Requests.Departments;

public sealed record CreateDepartmentRequest(
    string Name,
    string Identifier,
    Guid? ParentId,
    IEnumerable<Guid> LocationIds);
