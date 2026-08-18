using CSharpFunctionalExtensions;
using DirectoryService.Application.Features.Locations;
using DirectoryService.Domain.Entity;
using DirectoryService.Domain.Shared;
using System.Linq.Expressions;

namespace DirectoryService.Infrastructure.Postgres.Repositories;

public class LocationRepository : ILocationRepository
{
    public Task<Guid> Add(Location location, CancellationToken cancellationToken = default) => throw new NotImplementedException();
    public Task<Result<Location, Error>> GetByAsync(Expression<Func<Location, bool>> predicate, CancellationToken cancellationToken = default) => throw new NotImplementedException();
}
