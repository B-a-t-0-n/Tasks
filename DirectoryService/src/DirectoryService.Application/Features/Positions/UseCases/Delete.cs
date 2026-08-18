using CSharpFunctionalExtensions;
using DirectoryService.Application.Abstractions;
using DirectoryService.Application.Endpoints;
using DirectoryService.Domain.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;


namespace DirectoryService.Application.Features.Positions.UseCases;

public sealed record DeletePositionCommand(Guid Id) : ICommand;

public sealed class DeletePositionEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/positions/{id:guid}", async Task<EndpointResult<Guid>> (
            [FromRoute] Guid id,
            [FromServices] DeletePositionHandler handler,
            CancellationToken ct) =>
        {
            var command = new DeletePositionCommand(id);

            return await handler.Handle(command, ct);
        })
        .WithTags("Positions");
    }
}

public sealed class DeletePositionHandler(ILogger<DeletePositionHandler> logger) : ICommandHandler<Guid, DeletePositionCommand>
{
    private readonly ILogger<DeletePositionHandler> _logger = logger;

    public async Task<Result<Guid,Error>> Handle(DeletePositionCommand command, CancellationToken ct)
    {
        _logger.LogInformation("Handle method delete");

        return command.Id;
    }
}
