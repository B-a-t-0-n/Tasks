using DirectoryService.Application;
using DirectoryService.Application.Endpoints;
using DirectoryService.Infrastructure.Postgres;
using DirectoryService.Web.EndpointsSettings;
using Serilog;
using Serilog.Exceptions;
namespace DirectoryService.Web.Configuration;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddApplication(configuration)
            .AddInfrastructurePostgres(configuration)
            .AddSerilogLogging(configuration)
            .AddEndpointsApiExplorer()
            .AddSwaggerGen()
            .AddEndpoints(typeof(IEndpoint).Assembly);
        ;
    }

    private static IServiceCollection AddSerilogLogging(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddSerilog((sp, lc) => lc
            .ReadFrom.Configuration(configuration)
            .ReadFrom.Services(sp)
            .Enrich.FromLogContext()
            .Enrich.WithExceptionDetails()
            .Enrich.WithProperty("ServiceName", "DirectoryService"));
    }
}
