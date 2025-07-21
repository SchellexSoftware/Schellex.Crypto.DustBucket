using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Schellex.Crypto.Coinbase.AdvancedApi.Extensions;
using Schellex.Crypto.DustBucket.BusinessLogic.Extensions;
using Schellex.Crypto.DustBucket.Console;

try
{
    var builder = Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((hostingContext, config) =>
        {
            var env = hostingContext.HostingEnvironment;

            config
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true) // NOTE: DO NOT COMMIT THIS FILE
                .AddEnvironmentVariables();
        })
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
    await host.RunAsync();
    
    Environment.Exit(0);
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
    Environment.Exit(1);
}