using CSharpFunctionalExtensions;
using DirectoryService.Application.Features.Departments;
using DirectoryService.Domain.Entity;
using DirectoryService.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DirectoryService.Infrastructure.Postgres.Repositories;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly DirectoryDbContext _dbContext;

    public DepartmentRepository(DirectoryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Department department, CancellationToken cancellationToken = default)
    {
        await _dbContext.AddAsync(department, cancellationToken);
    }
    public async Task<Result<Department, Error>> GetByAsync(Expression<Func<Department, bool>> predicate, CancellationToken cancellationToken = default)
    {
        var department = await _dbContext.Departments.FirstOrDefaultAsync(predicate, cancellationToken);

        if (department is null)
            return GeneralErrors.NotFound(null, "департамент");

        return department;
    }
}
