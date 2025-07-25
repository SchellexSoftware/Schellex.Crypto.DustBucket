namespace Schellex.Crypto.Coinbase.AdvancedApi.Configuration.Models;

/// <summary>
/// Represents configuration values required to authenticate and interact with the Coinbase Advanced API.
/// </summary>
public record CoinbaseSettings
{
    /// <summary>
    /// The base URL of the Coinbase API (e.g., https://api.coinbase.com).
    /// </summary>
    public required string BaseUrl { get; set; }

    /// <summary>
    /// The API key used to authenticate with the Coinbase Advanced API.
    /// </summary>
    public required string ApiKey { get; set; }

    /// <summary>
    /// The PEM-encoded private key used to sign JWTs for Coinbase authentication.
    /// </summary>
    public required string Secret { get; set; }

    /// <summary>
    /// The ID of the Coinbase portfolio to use for executing trades.
    /// </summary>
    public required string PortfolioId { get; set; }
}
