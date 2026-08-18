using CSharpFunctionalExtensions;
using DirectoryService.Application.Abstractions;
using DirectoryService.Application.Endpoints;
using DirectoryService.Contracts.Requests.Positions;
using DirectoryService.Domain.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace DirectoryService.Application.Features.Positions.UseCases;

public sealed record UpdatePositionCommand(Guid Id, UpdatePositionRequest Request) : ICommand;

public sealed class UpdatePositionEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("/positions/{id:guid}", async Task<EndpointResult<Guid>> (
            [FromRoute] Guid id,
            [FromBody] UpdatePositionRequest request,
            [FromServices] UpdatePositionHandler handler,
            CancellationToken ct) =>
        {
            var command = new UpdatePositionCommand(id, request);

            return await handler.Handle(command, ct);
        })
        .WithTags("Positions");
    }
}

public sealed class UpdatePositionHandler(ILogger<UpdatePositionHandler> logger) : ICommandHandler<Guid, UpdatePositionCommand>
{
    private readonly ILogger<UpdatePositionHandler> _logger = logger;

    public async Task<Result<Guid, Error>> Handle(UpdatePositionCommand command, CancellationToken ct) 
    {
        _logger.LogInformation("Handle method Update");

        return command.Id;
    }
}
