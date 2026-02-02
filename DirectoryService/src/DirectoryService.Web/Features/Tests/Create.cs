using DirectoryService.Web.EndpointsSettings;

namespace DirectoryService.Web.Features.Tests;

public class CreateEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/create", async (CreateHandler handler) =>
        {
            await handler.Handle();
        });
    }
}

public sealed class CreateHandler
{
    private readonly ILogger<CreateHandler> _logger ;

    public CreateHandler(ILogger<CreateHandler> logger)
    {
        _logger = logger;
    }

    public async Task Handle()
    {
        _logger.LogInformation("Handle method Create");
        await Task.CompletedTask;
    }
}
