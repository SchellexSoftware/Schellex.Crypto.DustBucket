using Microsoft.Extensions.Hosting;
using Schellex.Crypto.DustBucket.Interfaces;

namespace Schellex.Crypto.DustBucket.Console
{
    /// <summary>
    /// AppService is a background service that runs as part of the .NET Generic Host lifecycle.
    /// It is responsible for invoking the core AppManager logic exactly once at application startup,
    /// typically to perform a one-off automated task.
    ///
    /// This service is intended to be registered with dependency injection and started automatically
    /// when the host runs (e.g., from Program.cs using IHostBuilder). Once the RunAsync method completes,
    /// the service gracefully signals the host to shut down, making it ideal for console apps or jobs
    /// that should execute and exit cleanly without manual intervention.
    ///
    /// To use this service:
    /// - Register your IAppManager and AppService with the service collection.
    /// - Ensure the host is started using `Host.CreateDefaultBuilder(...).Run();`
    /// - The app will run the main logic once and exit automatically.
    /// 
    /// See also: https://learn.microsoft.com/en-us/dotnet/core/extensions/generic-host?tabs=appbuilder
    /// </summary>
    public class AppService : HostedService
    {
        private readonly IHostApplicationLifetime _host;
        private readonly IDustBucketManager _appManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppService"/> class.
        /// </summary>
        /// <param name="host">Provides application lifetime management.</param>
        /// <param name="appManager">The manager responsible for executing the main business logic.</param>
        public AppService(IHostApplicationLifetime host, IDustBucketManager appManager)
        {
            _host = host;
            _appManager = appManager;
        }

        /// <summary>
        /// Executes the application's main logic and stops the host when complete.
        /// </summary>
        /// <param name="cancellationToken">Token for cancellation of the background task.</param>
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            await _appManager.RunAsync(); // Note: Actual logic implementation begins here.
            _host.StopApplication();
        }
    }
}