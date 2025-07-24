/// <summary>
/// The Program class serves as the entry point for the DustBucket console application.
/// It sets up the .NET Generic Host, loads configuration from various sources, 
/// registers required services, and starts the background hosted AppService.
/// </summary>

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Schellex.Crypto.Coinbase.AdvancedApi.Extensions;
using Schellex.Crypto.DustBucket.BusinessLogic.Extensions;
using Schellex.Crypto.DustBucket.Console;

try
{
    // Configures and builds the host for running the DustBucket application.
    var builder = Host.CreateDefaultBuilder(args)

        // Adds JSON configuration files, including support for local overrides, 
        // and loads environment variables.
        .ConfigureAppConfiguration((hostingContext, config) =>
        {
            var env = hostingContext.HostingEnvironment;
            var basePath = AppContext.BaseDirectory;

            config
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true) // NOTE: DO NOT COMMIT THIS FILE
                .AddEnvironmentVariables();
        })
        
        // Registers all required services with the DI container, including hosted services
        // and application-specific services for business and API logic.
        .ConfigureServices((context, services) =>
        {
            services.AddHostedService<AppService>();
            services.AddSingleton<IConfiguration>(context.Configuration);
            services.AddSingleton<IServiceCollection>(services);

            #region App Specific Services
            services.AddDustBucketServices(context.Configuration);
            services.AddCoinbaseServices(context.Configuration);
            #endregion
        });

    var host = builder.Build();

    // Starts the application host and runs all background services.
    await host.RunAsync();

    // Ensures a clean shutdown after the application completes.
    Environment.Exit(0);
}
catch (Exception ex)
{
    // Catches any fatal error during host startup or execution and logs it before exiting.
    Console.WriteLine($"An error occurred: {ex.Message}");
    Environment.Exit(1);
}