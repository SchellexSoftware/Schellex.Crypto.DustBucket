using Schellex.Crypto.Coinbase.AdvancedApi.Interfaces.Managers;
using Schellex.Crypto.DustBucket.Interfaces;

namespace Schellex.Crypto.DustBucket.BusinessLogic.Managers
{
    public class DustBucketManager : IDustBucketManager
    {
        private readonly ICoinbaseManager _coinbaseManager;

        public DustBucketManager(ICoinbaseManager coinbaseManager)
        {
            _coinbaseManager = coinbaseManager;
        }

        public async Task RunAsync()
        {
            // await GetAccountInfoAsync();
            // await GetCurrentPriceAsync();
            await PlaceOrderAsync();
        }

        private async Task PlaceOrderAsync()
        {
            var orderResult = await _coinbaseManager.CreateOrderAsync("BTC-USDC", 5.00m, "BUY");
            Console.WriteLine($"Order Result: {orderResult}");
        }

        private async Task GetCurrentPriceAsync()
        {
            var currentPriceResult = await _coinbaseManager.GetCurrentPriceAsync("BTC-USDC");
            Console.WriteLine($"Current Price: {currentPriceResult}");
        }

        private async Task GetAccountInfoAsync()
        {
            var accountInfoResult = await _coinbaseManager.GetAccountInfoAsync();
            Console.WriteLine($"Current Price: {accountInfoResult}");
        }
    }
}