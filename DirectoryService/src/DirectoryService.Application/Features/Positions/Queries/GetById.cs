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

namespace DirectoryService.Application.Features.Positions.Queries;

public sealed record GetPositionByIdQuery(Guid Id) : IQuery;

public sealed class GetPositionByIdEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/positions/{id:guid}", async Task<EndpointResult<PositionResponce?>> (
            [FromRoute] Guid id,
            [FromServices] GetPositionByIdHandler handler,
            CancellationToken ct) =>
        {
            var query = new GetPositionByIdQuery(id);

            return await handler.Handle(query, ct);
        })
        .WithTags("Positions");
    }
}

public sealed class GetPositionByIdHandler(ILogger<GetPositionByIdHandler> logger) : IQueryHandlerWithResult<PositionResponce?, GetPositionByIdQuery>
{
    private readonly ILogger<GetPositionByIdHandler> _logger = logger;

    public async Task<Result<PositionResponce?, Error>> Handle(GetPositionByIdQuery query, CancellationToken ct)
    {
        _logger.LogInformation("Handle method get");

        return new PositionResponce(Guid.CreateVersion7(), "", "", DateTime.UtcNow, DateTime.UtcNow);
    }
}
