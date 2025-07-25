using Schellex.Crypto.Coinbase.AdvancedApi.Interfaces.Managers;
using Schellex.Crypto.Coinbase.AdvancedApi.Interfaces.Services;

namespace Schellex.Crypto.Coinbase.AdvancedApi.Managers
{
    /// <summary>
    /// Coordinates Coinbase-related operations by delegating requests to service implementations for accounts, prices, and orders.
    /// Acts as a facade for simplified access to Coinbase API interactions.
    /// </summary>
    public class CoinbaseManager : ICoinbaseManager
    {
        private readonly IAccountService _accountService;
        private readonly ICryptoService _cryptoService;
        private readonly IOrderService _orderService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CoinbaseManager"/> class with injected service dependencies.
        /// </summary>
        /// <param name="accountService">The service responsible for retrieving account information.</param>
        /// <param name="cryptoService">The service responsible for fetching current crypto prices.</param>
        /// <param name="orderService">The service responsible for placing buy/sell orders.</param>
        public CoinbaseManager(IAccountService accountService, ICryptoService cryptoService, IOrderService orderService)
        {
            _accountService = accountService;
            _cryptoService = cryptoService;
            _orderService = orderService;
        }

        /// <summary>
        /// Retrieves the authenticated Coinbase account's information.
        /// </summary>
        /// <returns>A task that returns a JSON string with account details.</returns>
        public async Task<string> GetAccountInfoAsync()
        {
            return await _accountService.GetAccountInfoAsync();
        }

        /// <summary>
        /// Retrieves the current price for the specified currency pair.
        /// </summary>
        /// <param name="currencyPair">The trading pair (e.g., "BTC-USDC").</param>
        /// <returns>A task that returns a JSON string with current price data.</returns>
        public async Task<string> GetCurrentPriceAsync(string currencyPair)
        {
            return await _cryptoService.GetCurrentPriceAsync(currencyPair);
        }

        /// <summary>
        /// Places a new order for the specified currency pair and side.
        /// </summary>
        /// <param name="currencyPair">The trading pair (e.g., "BTC-USDC").</param>
        /// <param name="amount">The amount to spend or size to order, depending on quote/base logic.</param>
        /// <param name="side">The order side, either "BUY" or "SELL".</param>
        /// <returns>A task that returns a JSON string with the order confirmation.</returns>
        public async Task<string> CreateOrderAsync(string currencyPair, decimal amount, string side)
        {
            return await _orderService.CreateOrderAsync(currencyPair, amount, side);
        }
    }
}