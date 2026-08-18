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

public sealed record GetPositionsQuery() : IQuery;

public sealed class GetPositionsEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/positions", async Task<EndpointResult<IEnumerable<PositionResponse>>> (
            [FromServices] GetPositionsHandler handler,
            CancellationToken ct) =>
        {
            var query = new GetPositionsQuery();

            return await handler.Handle(query, ct);
        })
        .WithTags("Positions");
    }
}

public sealed class GetPositionsHandler(ILogger<GetPositionsHandler> logger) : IQueryHandlerWithResult<IEnumerable<PositionResponse>, GetPositionsQuery>
{
    private readonly ILogger<GetPositionsHandler> _logger = logger;

    public async Task<Result<IEnumerable<PositionResponse>, Error>> Handle(GetPositionsQuery query, CancellationToken ct)
    {
        _logger.LogInformation("Handle method get");

        var list = new List<PositionResponse>();

        return list;
    }

}
