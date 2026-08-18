using CSharpFunctionalExtensions;
using DirectoryService.Application.Abstractions;
using DirectoryService.Application.Endpoints;
using DirectoryService.Application.Features.Departments.Queries;
using DirectoryService.Contracts.DTOs;
using DirectoryService.Contracts.Response;
using DirectoryService.Domain.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace DirectoryService.Application.Features.Locations.Queries;

public sealed record GetLocationByIdQuery(Guid Id) : IQuery;

public sealed class GetLocationByIdEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/locations/{id:guid}", async Task<EndpointResult<LocationResponce?>> (
            [FromRoute] Guid id,
            [FromServices] GetLocationByIdHandler handler,
            CancellationToken ct) =>
        {
            var query = new GetLocationByIdQuery(id);

            return await handler.Handle(query, ct);
        })
        .WithTags("Locations");
    }
}

public sealed class GetLocationByIdHandler(ILogger<GetLocationByIdHandler> logger) : IQueryHandlerWithResult<LocationResponce?, GetLocationByIdQuery>
{
    private readonly ILogger<GetLocationByIdHandler> _logger = logger;

    public async Task<Result<LocationResponce?, Error>> Handle(GetLocationByIdQuery query, CancellationToken ct)
    {
        _logger.LogInformation("Handle method get");

        return new LocationResponce(Guid.CreateVersion7(), "", new AddressDTO("", "", "", "", ""), "", DateTime.UtcNow, DateTime.UtcNow);
    }
}
