using CSharpFunctionalExtensions;
using DirectoryService.Application.Abstractions;
using DirectoryService.Application.Endpoints;
using DirectoryService.Contracts.Requests.Locations;
using DirectoryService.Domain.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace DirectoryService.Application.Features.Locations.UseCases;

public sealed record UpdateLocationCommand(Guid Id, UpdateLocationRequest Request) : ICommand;

public sealed class UpdateLocationEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("/locations/{id:guid}", async Task<EndpointResult<Guid>> (
            [FromRoute] Guid id,
            [FromBody] UpdateLocationRequest request,
            [FromServices] UpdateLocationHandler handler,
            CancellationToken ct) =>
        {
            var command = new UpdateLocationCommand(id, request);

            return await handler.Handle(command, ct);
        })
        .WithTags("Locations");
    }
}

public sealed class UpdateLocationHandler(ILogger<UpdateLocationHandler> logger) : ICommandHandler<Guid, UpdateLocationCommand>
{
    private readonly ILogger<UpdateLocationHandler> _logger = logger;

    public async Task<Result<Guid, Error>> Handle(UpdateLocationCommand command, CancellationToken ct) 
    {
        _logger.LogInformation("Handle method Update");

        return command.Id;
    }
}
