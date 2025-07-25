using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Schellex.Crypto.Coinbase.AdvancedApi.Configuration.Interfaces;
using Schellex.Crypto.Coinbase.AdvancedApi.Configuration.Models;
using Schellex.Crypto.Coinbase.AdvancedApi.Interfaces.Managers;
using Schellex.Crypto.Coinbase.AdvancedApi.Interfaces.Services;
using Schellex.Crypto.Coinbase.AdvancedApi.Managers;
using Schellex.Crypto.Coinbase.AdvancedApi.Services;

namespace Schellex.Crypto.Coinbase.AdvancedApi.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddCoinbaseServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IAppSettings, AppSettings>();
        services.AddTransient<ICoinbaseManager, CoinbaseManager>();
        services.AddTransient<IAccountService, AccountService>();
        services.AddTransient<ICryptoService, CryptoService>();
        services.AddTransient<IOrderService, OrderService>();

        return services;
    }
}