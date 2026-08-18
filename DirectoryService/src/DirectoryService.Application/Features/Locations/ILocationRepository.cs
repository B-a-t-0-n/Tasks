using CSharpFunctionalExtensions;
using DirectoryService.Domain.Entity;
using DirectoryService.Domain.Shared;
using System.Linq.Expressions;

namespace DirectoryService.Application.Features.Locations;

public interface ILocationRepository
{
    Task<Result<Location, Error>> GetByAsync(
        Expression<Func<Location, bool>> predicate,
        CancellationToken cancellationToken = default);
    Task<Guid> Add(Location location, CancellationToken cancellationToken = default);
}
