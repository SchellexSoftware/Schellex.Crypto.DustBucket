namespace Schellex.Crypto.Coinbase.AdvancedApi.Interfaces.Managers;

/// <summary>
/// Defines Coinbase Advanced API operations for account information, price lookup, and order creation.
/// </summary>
public interface ICoinbaseManager
{
    /// <summary>
    /// Retrieves account information from the Coinbase Advanced API.
    /// </summary>
    /// <returns>A JSON string representing the account data.</returns>
    Task<string> GetAccountInfoAsync();

    /// <summary>
    /// Retrieves the current price for the given currency pair (e.g., "BTC-USDC").
    /// </summary>
    /// <param name="currencyPair">The product pair symbol.</param>
    /// <returns>A JSON string representing the price data.</returns>
    Task<string> GetCurrentPriceAsync(string currencyPair);

    /// <summary>
    /// Creates a new market order with the specified parameters.
    /// </summary>
    /// <param name="currencyPair">The product pair symbol (e.g., "BTC-USDC").</param>
    /// <param name="amount">The quote amount to spend (e.g., $5).</param>
    /// <param name="side">"BUY" or "SELL".</param>
    /// <returns>A JSON string representing the order response.</returns>
    Task<string> CreateOrderAsync(string currencyPair, decimal amount, string side);
}
