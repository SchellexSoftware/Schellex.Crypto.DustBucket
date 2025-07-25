using System.Text.Json;
using Schellex.Crypto.Coinbase.AdvancedApi.Configuration.Interfaces;
using Schellex.Crypto.Coinbase.AdvancedApi.Interfaces.Services;

namespace Schellex.Crypto.Coinbase.AdvancedApi.Services;

/// <summary>
/// Provides functionality to create cryptocurrency orders using the Coinbase Advanced API.
/// </summary>
public class OrderService : ServiceBase, IOrderService
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OrderService"/> class.
    /// </summary>
    /// <param name="appSettings">Injected application settings interface.</param>
    public OrderService(IAppSettings appSettings) : base(appSettings) { }

    /// <summary>
    /// Creates a new market order for the specified product using immediate-or-cancel execution.
    /// </summary>
    /// <param name="productId">The product pair identifier (e.g., "BTC-USDC").</param>
    /// <param name="price">The amount to spend in quote currency.</param>
    /// <param name="side">The order side, either "BUY" or "SELL".</param>
    /// <returns>A task that returns a JSON string with the API response.</returns>
    public async Task<string> CreateOrderAsync(string productId, decimal price, string side)
    {
        var endpoint = "brokerage/orders";

        var orderBody = new
        {
            client_order_id = Guid.NewGuid().ToString(),
            product_id = productId,
            side = side,
            order_configuration = new
            {
                market_market_ioc = new
                {
                    quote_size = price.ToString("F2"),
                    rfq_disabled = true
                }
            },
        };

        var json = JsonSerializer.Serialize(orderBody);

        var result = await CallApiPOSTAsync(endpoint, json);
        return result;
    }
}
