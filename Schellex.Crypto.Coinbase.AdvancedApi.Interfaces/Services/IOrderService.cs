namespace Schellex.Crypto.Coinbase.AdvancedApi.Interfaces.Services;

public interface IOrderService
{
    Task<string> CreateOrderAsync(string productId, decimal price, string side);
}
