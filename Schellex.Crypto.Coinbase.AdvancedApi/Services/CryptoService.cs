using Schellex.Crypto.Coinbase.AdvancedApi.Configuration.Interfaces;
using Schellex.Crypto.Coinbase.AdvancedApi.Interfaces.Services;

namespace Schellex.Crypto.Coinbase.AdvancedApi.Services;

/// <summary>
/// Provides methods to retrieve cryptocurrency pricing data from the Coinbase Advanced API.
/// </summary>
public class CryptoService : ServiceBase, ICryptoService
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CryptoService"/> class.
    /// </summary>
    /// <param name="appSettings">Injected application settings used for API authentication.</param>
    public CryptoService(IAppSettings appSettings) : base(appSettings) { }

    /// <summary>
    /// Retrieves the current price for the specified product pair from the Coinbase Advanced API.
    /// </summary>
    /// <param name="productId">The product pair identifier (e.g., "BTC-USDC").</param>
    /// <returns>A task that returns a JSON string representing the current price data.</returns>
    public async Task<string> GetCurrentPriceAsync(string productId)
    {
        string endpoint = $"brokerage/products/{productId}";
        var result = await CallApiGETAsync(endpoint);
        return result;
    }
}
