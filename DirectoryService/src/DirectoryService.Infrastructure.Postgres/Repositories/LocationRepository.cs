using CSharpFunctionalExtensions;
using DirectoryService.Application.Features.Locations;
using DirectoryService.Domain.Entity;
using DirectoryService.Domain.Shared;
using DirectoryService.Domain.ValueObjects.IDs;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DirectoryService.Infrastructure.Postgres.Repositories;

public class LocationRepository : ILocationRepository
{
    private readonly DirectoryDbContext _dbContext;

    public LocationRepository(DirectoryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Location location, CancellationToken cancellationToken = default)
    {
        await _dbContext.AddAsync(location, cancellationToken);
    }
    public async Task<Result<Location, Error>> GetByAsync(Expression<Func<Location, bool>> predicate, CancellationToken cancellationToken = default)
    {
        var location = await _dbContext.Locations.FirstOrDefaultAsync(predicate, cancellationToken);

        if (location is null)
            return GeneralErrors.NotFound(null, "локацию");

        return location;
    }

    public async Task<UnitResult<Error>> AnyByIds(
        IEnumerable<Guid> ids,
        CancellationToken cancellationToken = default)
    {
        LocationId[] locationIds = ids
            .Distinct()
            .Select(LocationId.Create)
            .ToArray();

        var existingLocationsCount = await _dbContext.Locations
            .CountAsync(
                location => !location.IsDeleted && locationIds.Contains(location.Id),
                cancellationToken);

        if (existingLocationsCount != locationIds.Length)
            return GeneralErrors.NotFound(entityName: "одну или несколько локаций");

        return UnitResult.Success<Error>();
    }
}
