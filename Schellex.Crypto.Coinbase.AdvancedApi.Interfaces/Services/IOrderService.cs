namespace Schellex.Crypto.Coinbase.AdvancedApi.Interfaces.Services;

/// <summary>
/// Defines operations for creating orders on the Coinbase Advanced API.
/// </summary>
public interface IOrderService
{
    /// <summary>
    /// Creates a new market or limit order with the specified parameters.
    /// </summary>
    /// <param name="productId">The product pair identifier (e.g., "BTC-USDC").</param>
    /// <param name="price">The price at which to place the order.</param>
    /// <param name="side">The order side, either "BUY" or "SELL".</param>
    /// <returns>A task that returns a JSON string representing the API response.</returns>
    Task<string> CreateOrderAsync(string productId, decimal price, string side);
}
