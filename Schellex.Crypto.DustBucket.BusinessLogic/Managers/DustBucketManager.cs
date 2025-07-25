using Schellex.Crypto.Coinbase.AdvancedApi.Interfaces.Managers;
using Schellex.Crypto.DustBucket.Interfaces;

namespace Schellex.Crypto.DustBucket.BusinessLogic.Managers
{
    /// <summary>
    /// Executes the DustBucket workflow: placing a scheduled order and optionally fetching
    /// price or account data via the CoinbaseManager facade.
    /// </summary>
    public class DustBucketManager : IDustBucketManager
    {
        private readonly ICoinbaseManager _coinbaseManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="DustBucketManager"/> class.
        /// </summary>
        public DustBucketManager(ICoinbaseManager coinbaseManager)
        {
            _coinbaseManager = coinbaseManager;
        }

        /// <summary>
        /// Runs the DustBucket logic asynchronously (currently only places an order).
        /// </summary>
        public async Task RunAsync()
        {
            //await GetAccountInfoAsync();
            // await GetCurrentPriceAsync();
            await PlaceOrderAsync();
        }

        /// <summary>
        /// Places a market buy order for BTC using USDC via the CoinbaseManager.
        /// </summary>
        private async Task PlaceOrderAsync()
        {
            var orderResult = await _coinbaseManager.CreateOrderAsync("BTC-USDC", 5.00m, "BUY");
            Console.WriteLine($"Order Result: {orderResult}");
        }

        /// <summary>
        /// Fetches the current price for BTC-USDC using the CoinbaseManager.
        /// </summary>
        private async Task GetCurrentPriceAsync()
        {
            var currentPriceResult = await _coinbaseManager.GetCurrentPriceAsync("BTC-USDC");
            Console.WriteLine($"Current Price: {currentPriceResult}");
        }

        /// <summary>
        /// Retrieves account information using the CoinbaseManager.
        /// </summary>
        private async Task GetAccountInfoAsync()
        {
            var accountInfoResult = await _coinbaseManager.GetAccountInfoAsync();
            Console.WriteLine($"Account Info: {accountInfoResult}");
        }
    }
}