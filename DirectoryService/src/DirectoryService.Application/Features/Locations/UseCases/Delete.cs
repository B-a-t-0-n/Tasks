using CSharpFunctionalExtensions;
using DirectoryService.Application.Abstractions;
using DirectoryService.Application.Endpoints;
using DirectoryService.Domain.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;


namespace DirectoryService.Application.Features.Locations.UseCases;

public sealed record DeleteLocationCommand(Guid Id) : ICommand;

public sealed class DeleteLocationEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/locations/{id:guid}", async Task<EndpointResult<Guid>> (
            [FromRoute] Guid id,
            [FromServices] DeleteLocationHandler handler,
            CancellationToken ct) =>
        {
            var command = new DeleteLocationCommand(id);

            return await handler.Handle(command, ct);
        })
        .WithTags("Locations");
    }
}

public sealed class DeleteLocationHandler(ILogger<DeleteLocationHandler> logger) : ICommandHandler<Guid, DeleteLocationCommand>
{
    private readonly ILogger<DeleteLocationHandler> _logger = logger;

    public async Task<Result<Guid,Error>> Handle(DeleteLocationCommand command, CancellationToken ct)
    {
        _logger.LogInformation("Handle method delete");

        return command.Id;
    }
}
