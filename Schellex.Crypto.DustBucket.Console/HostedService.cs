using Microsoft.Extensions.Hosting;

namespace Schellex.Crypto.DustBucket.Console
{
    /// <summary>
    /// An abstract base class for implementing background services using .NET Generic Host.
    /// This class manages the lifecycle of a hosted service by wiring up StartAsync and StopAsync
    /// to an abstract ExecuteAsync method that must be implemented by derived classes.
    /// 
    /// Unlike traditional background threads, this pattern integrates cleanly into .NET hosting infrastructure,
    /// supporting graceful shutdown, cancellation, and dependency injection.
    /// 
    /// Usage:
    /// - Inherit from HostedService and implement ExecuteAsync(CancellationToken)
    /// - Register the derived class with the service collection (e.g., in Program.cs)
    /// - The host will automatically start and stop the service as part of its lifecycle
    /// </summary>
    public abstract class HostedService : IHostedService
    {
        private Task? _executingTask;
        private CancellationTokenSource? _cancellationTokenSource;

        /// <summary>
        /// Called by the host to start the background service.
        /// This method creates a linked CancellationTokenSource and starts the execution task.
        /// </summary>
        /// <param name="cancellationToken">A token that indicates when the host is shutting down.</param>
        /// <returns>A Task that completes when the service has started.</returns>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            // Begin executing the core background logic defined by the subclass.
            _executingTask = ExecuteAsync(_cancellationTokenSource.Token);

            // If the task has already completed (synchronously), return it.
            // Otherwise, return a placeholder to indicate startup has completed.
            return _executingTask.IsCompleted ? _executingTask : Task.CompletedTask;
        }

        /// <summary>
        /// Called by the host to stop the background service.
        /// This method cancels the execution token and waits for the executing task to finish.
        /// </summary>
        /// <param name="cancellationToken">A token that indicates when shutdown should no longer be graceful.</param>
        /// <returns>A Task that completes when the service has stopped.</returns>
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            // If the service never started or already completed, skip shutdown.
            if (_executingTask == null)
            {
                return;
            }

            // Signal cancellation to the executing task.
            _cancellationTokenSource?.Cancel();

            // Wait for either the task to finish or cancellation to force exit.
            await Task.WhenAny(_executingTask, Task.Delay(-1, cancellationToken));

            // Throw if cancellation was requested to bubble up properly.
            cancellationToken.ThrowIfCancellationRequested();
        }

        /// <summary>
        /// When implemented in a derived class, contains the logic that should be run in the background.
        /// This method is called once at startup and is expected to complete either:
        /// - When the task finishes (e.g., one-off job)
        /// - Or when the token is canceled (e.g., long-running loop with shutdown support)
        /// </summary>
        /// <param name="cancellationToken">Token that signals when execution should stop.</param>
        /// <returns>A Task representing the background operation.</returns>
        protected abstract Task ExecuteAsync(CancellationToken cancellationToken);
    }
}