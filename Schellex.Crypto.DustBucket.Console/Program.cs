
using Schellex.Crypto.Coinbase.AdvancedApi.Models;
using Schellex.Crypto.Coinbase.AdvancedApi.Managers;
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile("appsettings.local.json", optional: true) // Note: Do not commit this file.
    .Build();

var settings = config.GetSection("Coinbase").Get<CoinbaseSettings>()!;
var coinbaseManager = new CoinbaseManager(settings);

var accountInfoResult = await coinbaseManager.GetAccountInfoAsync();
Console.WriteLine($"Current Price: {accountInfoResult}");

// var currentPriceResult = await coinbaseManager.GetCurrentPriceAsync("BTC-USDC");
// Console.WriteLine($"Current Price: {currentPriceResult}");

// var orderResult = await coinbaseManager.CreateOrderAsync("BTC-USDC", 5.00m, "BUY");
// Console.WriteLine($"Order Result: {orderResult}");