using System.Text.Json;
using Schellex.Crypto.Coinbase.AdvancedApi.Configuration.Interfaces;
using Schellex.Crypto.Coinbase.AdvancedApi.Interfaces.Services;

namespace Schellex.Crypto.Coinbase.AdvancedApi.Services;

public class OrderService : ServiceBase, IOrderService
{
    public OrderService(IAppSettings appSettings) : base(appSettings) { }

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
