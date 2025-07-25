namespace Schellex.Crypto.Coinbase.AdvancedApi.Interfaces.Services;

/// <summary>
/// Defines operations for retrieving account information from the Coinbase Advanced API.
/// </summary>
public interface IAccountService
{
    /// <summary>
    /// Retrieves account information asynchronously.
    /// </summary>
    /// <returns>A task that returns a JSON string containing account details.</returns>
    Task<string> GetAccountInfoAsync();
}
