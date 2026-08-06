using DirectoryService.Web.Configuration;
using Serilog;
using System.Globalization;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console(formatProvider: CultureInfo.InvariantCulture)
    .CreateLogger();

try
{
    Log.Information("Starting web application");

    var builder = WebApplication.CreateBuilder(args);

    string environment = builder.Environment.EnvironmentName;

    builder.Configuration
        .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
        .AddEnvironmentVariables(prefix: "ASPNETCORE_")
        .AddEnvironmentVariables()
        .AddCommandLine(args);

    builder.Services.AddConfiguration(builder.Configuration);

    builder.Services.AddLogging();

    var app = builder.Build();

    app.Configure();

    app.Run();

}
catch (Exception ex)
{
    Log.Error(ex, "Unhandled exception");
}
finally
{
    await Log.CloseAndFlushAsync(); 
}



