namespace Schellex.Crypto.Coinbase.AdvancedApi.Interfaces.Services;

public interface ICryptoService
{
    Task<string> GetCurrentPriceAsync(string productId);
}
