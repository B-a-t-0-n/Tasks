using CSharpFunctionalExtensions;
using DirectoryService.Domain.Entity;
using DirectoryService.Domain.Shared;
using System.Linq.Expressions;

namespace DirectoryService.Application.Features.Departments;

public interface IDepartmentRepository
{
    Task<Result<Department, Error>> GetByAsync(
        Expression<Func<Department, bool>> predicate,
        CancellationToken cancellationToken = default);
    Task Add(Department вepartment, CancellationToken cancellationToken = default);
}
