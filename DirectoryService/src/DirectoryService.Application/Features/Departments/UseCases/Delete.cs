using CSharpFunctionalExtensions;
using DirectoryService.Application.Abstractions;
using DirectoryService.Application.Endpoints;
using DirectoryService.Domain.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;


namespace DirectoryService.Application.Features.Departments.UseCases;

public sealed record DeleteDepartmentCommand(Guid Id) : ICommand;

public sealed class DeleteDepartmentEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/departments/{id:guid}", async Task<EndpointResult<Guid>> (
            [FromRoute] Guid id,
            [FromServices] DeleteDepartmentHandler handler,
            CancellationToken ct) =>
        {
            var command = new DeleteDepartmentCommand(id);

            return await handler.Handle(command, ct);
        })
        .WithTags("Departments");
    }
}

public sealed class DeleteDepartmentHandler(ILogger<DeleteDepartmentHandler> logger) : ICommandHandler<Guid, DeleteDepartmentCommand>
{
    private readonly ILogger<DeleteDepartmentHandler> _logger = logger;

    public async Task<Result<Guid,Error>> Handle(DeleteDepartmentCommand command, CancellationToken ct)
    {
        _logger.LogInformation("Handle method delete");

        return command.Id;
    }
}
