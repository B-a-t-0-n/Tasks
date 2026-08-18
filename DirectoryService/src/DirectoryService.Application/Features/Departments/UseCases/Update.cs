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

public sealed record UpdateDepartmentCommand(Guid Id, UpdateDepartmentRequest Request) : ICommand;

public sealed class UpdateDepartmentEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("/departments/{id:guid}", async Task<EndpointResult<Guid>> (
            [FromRoute] Guid id,
            [FromBody] UpdateDepartmentRequest request,
            [FromServices] UpdateDepartmentHandler handler,
            CancellationToken ct) =>
        {
            var command = new UpdateDepartmentCommand(id, request);

            return await handler.Handle(command, ct);
        })
        .WithTags("Departments");
    }
}

public sealed class UpdateDepartmentHandler(ILogger<UpdateDepartmentHandler> logger) : ICommandHandler<Guid, UpdateDepartmentCommand>
{
    private readonly ILogger<UpdateDepartmentHandler> _logger = logger;

    public async Task<Result<Guid, Error>> Handle(UpdateDepartmentCommand command, CancellationToken ct) 
    {
        _logger.LogInformation("Handle method Update");

        return command.Id;
    }
}
