using DirectoryService.Web.EndpointsSettings;
using Serilog;

namespace DirectoryService.Web.Configuration;

public static class AppExtensions
{
    public static IApplicationBuilder Configure(this WebApplication app)
    {
        app.UseSerilogRequestLogging();

        app.UseSwagger();
        app.UseSwaggerUI();

        RouteGroupBuilder apiGroup = app.MapGroup("/api/v1").WithOpenApi();
        app.MapEndpoints(apiGroup);

        return app;
    }
}
