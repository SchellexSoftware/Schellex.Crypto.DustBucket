namespace Schellex.Crypto.Coinbase.AdvancedApi.Interfaces.Managers;

public interface ICoinbaseManager
{
    Task<string> GetAccountInfoAsync();
    Task<string> GetCurrentPriceAsync(string currencyPair);
    Task<string> CreateOrderAsync(string currencyPair, decimal amount, string side);
}
