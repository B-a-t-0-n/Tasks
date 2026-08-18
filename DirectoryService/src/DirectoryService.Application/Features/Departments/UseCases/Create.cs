using CSharpFunctionalExtensions;
using DirectoryService.Application.Abstractions;
using DirectoryService.Application.Endpoints;
using DirectoryService.Contracts.Requests.Departments;
using DirectoryService.Domain.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace DirectoryService.Application.Features.Departments.UseCases;

public sealed record CreateDepartmentCommand(CreateDepartmentRequest Request) : ICommand;

public sealed class CreateDepartmentEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/departments", async Task<EndpointResult<Guid>> (
            [FromBody] CreateDepartmentRequest request,
            [FromServices] CreateDepartmentHandler handler,
            CancellationToken ct) =>
        {
            var command = new CreateDepartmentCommand(request);

            return await handler.Handle(command ,ct);
        })
        .WithTags("Departments");
    }
}

public sealed class CreateDepartmentHandler(ILogger<CreateDepartmentHandler> logger) : ICommandHandler<Guid ,CreateDepartmentCommand>
{
    private readonly ILogger<CreateDepartmentHandler> _logger = logger;

    public async Task<Result<Guid,Error>> Handle(CreateDepartmentCommand command, CancellationToken ct)
    {
        _logger.LogInformation("Handle method Create");

        return Guid.CreateVersion7();
    }
}


