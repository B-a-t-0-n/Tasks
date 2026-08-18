using CSharpFunctionalExtensions;
using DirectoryService.Application.Abstractions;
using DirectoryService.Application.Endpoints;
using DirectoryService.Contracts.Response;
using DirectoryService.Domain.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace DirectoryService.Application.Features.Locations.Queries;

public sealed record GetLocationsQuery() : IQuery;

public sealed class GetLocationsEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/locations", async Task<EndpointResult<IEnumerable<LocationResponce>>> (
            [FromServices] GetLocationsHandler handler,
            CancellationToken ct) =>
        {
            var query = new GetLocationsQuery();

            return await handler.Handle(query, ct);
        })
        .WithTags("Locations");
    }
}

public sealed class GetLocationsHandler(ILogger<GetLocationsHandler> logger) : IQueryHandlerWithResult<IEnumerable<LocationResponce>, GetLocationsQuery>
{
    private readonly ILogger<GetLocationsHandler> _logger = logger;

    public async Task<Result<IEnumerable<LocationResponce>, Error>> Handle(GetLocationsQuery query, CancellationToken ct)
    {
        _logger.LogInformation("Handle method get");

        var list = new List<LocationResponce>();

        return list;
    }

}
