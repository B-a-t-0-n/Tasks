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

namespace DirectoryService.Application.Features.Departments.Queries;

public sealed record GetDepartmentByIdQuery(Guid Id) : IQuery;

public sealed class GetDepartmentByIdEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/departments/{id:guid}", async Task<EndpointResult<DepartmentResponse?>> (
            [FromRoute] Guid id,
            [FromServices] GetDepartmentByIdHandler handler,
            CancellationToken ct) =>
        {
            var query = new GetDepartmentByIdQuery(id);

            return await handler.Handle(query, ct);
        })
        .WithTags("Departments");
    }
}

public sealed class GetDepartmentByIdHandler(ILogger<GetDepartmentByIdHandler> logger) : IQueryHandlerWithResult<DepartmentResponse?, GetDepartmentByIdQuery>
{
    private readonly ILogger<GetDepartmentByIdHandler> _logger = logger;

    public async Task<Result<DepartmentResponse?, Error>> Handle(GetDepartmentByIdQuery query, CancellationToken ct)
    {
        _logger.LogInformation("Handle method get");

        return new DepartmentResponse(Guid.CreateVersion7(), "", "", Guid.CreateVersion7(), "", 1, DateTime.UtcNow, DateTime.UtcNow);
    }
}
