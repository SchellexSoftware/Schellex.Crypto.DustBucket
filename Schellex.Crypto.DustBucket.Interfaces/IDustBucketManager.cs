/// <summary>
/// Defines a contract for executing the main DustBucket workflow,
/// such as retrieving price data, checking account status, and placing orders.
/// </summary>

namespace Schellex.Crypto.DustBucket.Interfaces;

/// <summary>
/// Runs the DustBucket logic asynchronously.
/// Intended for use in a hosted background context.
/// </summary>
public interface IDustBucketManager
{
    Task RunAsync();
}
