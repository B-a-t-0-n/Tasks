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

public sealed record GetDepartmentsQuery() : IQuery;

public sealed class GetDepartmentsEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/departments", async Task<EndpointResult<IEnumerable<DepartmentResponce>>> (
            [FromServices] GetDepartmentsHandler handler,
            CancellationToken ct) =>
        {
            var query = new GetDepartmentsQuery();

            return await handler.Handle(query, ct);
        })
        .WithTags("Departments");
    }
}

public sealed class GetDepartmentsHandler(ILogger<GetDepartmentsHandler> logger) : IQueryHandlerWithResult<IEnumerable<DepartmentResponce>, GetDepartmentsQuery>
{
    private readonly ILogger<GetDepartmentsHandler> _logger = logger;

    public async Task<Result<IEnumerable<DepartmentResponce>, Error>> Handle(GetDepartmentsQuery query, CancellationToken ct)
    {
        _logger.LogInformation("Handle method get");

        var list = new List<DepartmentResponce>();

        return list;
    }

}
