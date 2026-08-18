using DirectoryService.Application.Abstractions;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DirectoryService.Application;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddCommands()
            .AddQueries()
            .AddValidatorsFromAssembly(typeof(DependencyInjectionExtensions).Assembly);
    }

    private static IServiceCollection AddCommands(this IServiceCollection services)
    {
        services.Scan(scan => scan.FromAssemblies(typeof(DependencyInjectionExtensions).Assembly)
           .AddClasses(classes => classes
           .AssignableToAny(typeof(ICommandHandler<,>), typeof(ICommandHandler<>)))
           .AsSelfWithInterfaces()
           .WithScopedLifetime());

        return services;
    }

    private static IServiceCollection AddQueries(this IServiceCollection services)
    {
        services.Scan(scan => scan.FromAssemblies(typeof(DependencyInjectionExtensions).Assembly)
           .AddClasses(classes => classes
           .AssignableToAny(typeof(IQueryHandler<,>), typeof(IQueryHandlerWithResult<,>)))
           .AsSelfWithInterfaces()
           .WithScopedLifetime());

        return services;
    }


}
