namespace Schellex.Crypto.Coinbase.AdvancedApi.Interfaces.Services;

/// <summary>
/// Defines an abstraction for retrieving cryptocurrency pricing data.
/// </summary>
public interface ICryptoService
{
    /// <summary>
    /// Retrieves the current market price for the specified product pair (e.g., "BTC-USDC").
    /// </summary>
    /// <param name="productId">The product pair identifier.</param>
    /// <returns>A task that returns a JSON string with the current price information.</returns>
    Task<string> GetCurrentPriceAsync(string productId);
}
