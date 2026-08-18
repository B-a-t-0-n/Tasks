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

public sealed record CreateLocationCommand(CreateLocationRequest Request) : ICommand;

public sealed class CreateLocationEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/locations", async Task<EndpointResult<Guid>> (
            [FromBody] CreateLocationRequest request,
            [FromServices] CreateLocationHandler handler,
            CancellationToken ct) =>
        {
            var command = new CreateLocationCommand(request);

            return await handler.Handle(command ,ct);
        })
        .WithTags("Locations");
    }
}

public sealed class CreateLocationHandler(ILogger<CreateLocationHandler> logger) : ICommandHandler<Guid ,CreateLocationCommand>
{
    private readonly ILogger<CreateLocationHandler> _logger = logger;

    public async Task<Result<Guid,Error>> Handle(CreateLocationCommand command, CancellationToken ct)
    {
        _logger.LogInformation("Handle method Create");

        return Guid.CreateVersion7();
    }
}


