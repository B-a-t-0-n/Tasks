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

public sealed record CreatePositionCommand(CreatePositionRequest Request) : ICommand;

public sealed class CreatePositionEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/positions", async Task<EndpointResult<Guid>> (
            [FromBody] CreatePositionRequest request,
            [FromServices] CreatePositionHandler handler,
            CancellationToken ct) =>
        {
            var command = new CreatePositionCommand(request);

            return await handler.Handle(command ,ct);
        })
        .WithTags("Positions");
    }
}

public sealed class CreatePositionHandler(ILogger<CreatePositionHandler> logger) : ICommandHandler<Guid ,CreatePositionCommand>
{
    private readonly ILogger<CreatePositionHandler> _logger = logger;

    public async Task<Result<Guid,Error>> Handle(CreatePositionCommand command, CancellationToken ct)
    {
        _logger.LogInformation("Handle method Create");

        return Guid.CreateVersion7();
    }
}


