using Schellex.Crypto.Coinbase.AdvancedApi.Configuration.Interfaces;
using Schellex.Crypto.Coinbase.AdvancedApi.Interfaces.Services;

namespace Schellex.Crypto.Coinbase.AdvancedApi.Services;

public class CryptoService : ServiceBase, ICryptoService
{
    public CryptoService(IAppSettings appSettings) : base(appSettings) { }

    public async Task<string> GetCurrentPriceAsync(string productId)
    {
        string endpoint = $"brokerage/products/{productId}";
        var result = await CallApiGETAsync(endpoint);
        return result;
    }
}
