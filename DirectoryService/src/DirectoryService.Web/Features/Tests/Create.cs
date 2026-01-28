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
    public async Task Handle()
    {
        await Task.CompletedTask;
    }
}
