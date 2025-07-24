using Schellex.Crypto.Coinbase.AdvancedApi.Interfaces.Managers;
using Schellex.Crypto.Coinbase.AdvancedApi.Interfaces.Services;

namespace Schellex.Crypto.Coinbase.AdvancedApi.Managers
{
    public class CoinbaseManager : ICoinbaseManager
    {
        private readonly IAccountService _accountService;
        private readonly ICryptoService _cryptoService;
        private readonly IOrderService _orderService;

        public CoinbaseManager(IAccountService accountService, ICryptoService cryptoService, IOrderService orderService)
        {
            _accountService = accountService;
            _cryptoService = cryptoService;
            _orderService = orderService;
        }

        public async Task<string> GetAccountInfoAsync()
        {
            return await _accountService.GetAccountInfoAsync();
        }

        public async Task<string> GetCurrentPriceAsync(string currencyPair)
        {
            return await _cryptoService.GetCurrentPriceAsync(currencyPair);
        }

        public async Task<string> CreateOrderAsync(string currencyPair, decimal amount, string side)
        {
            return await _orderService.CreateOrderAsync(currencyPair, amount, side);
        }
    }
}