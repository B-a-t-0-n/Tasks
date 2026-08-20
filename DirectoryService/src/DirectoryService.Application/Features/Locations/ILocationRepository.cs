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
    Task Add(Location location, CancellationToken cancellationToken = default);

    Task<UnitResult<Error>> AnyByIds(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);
}
