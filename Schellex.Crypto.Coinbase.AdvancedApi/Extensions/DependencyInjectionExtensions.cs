using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Schellex.Crypto.Coinbase.AdvancedApi.Configuration.Interfaces;
using Schellex.Crypto.Coinbase.AdvancedApi.Configuration.Models;
using Schellex.Crypto.Coinbase.AdvancedApi.Interfaces.Managers;
using Schellex.Crypto.Coinbase.AdvancedApi.Interfaces.Services;
using Schellex.Crypto.Coinbase.AdvancedApi.Managers;
using Schellex.Crypto.Coinbase.AdvancedApi.Services;

namespace Schellex.Crypto.Coinbase.AdvancedApi.Extensions;

/// <summary>
/// Provides an extension method to register Coinbase API services into the dependency injection container.
/// </summary>
public static class DependencyInjectionExtensions
{
    /// <summary>
    /// Adds all required Coinbase Advanced API services and configuration to the service collection.
    /// </summary>
    /// <param name="services">The service collection to register services with.</param>
    /// <param name="configuration">The application's configuration instance.</param>
    /// <returns>The modified service collection with Coinbase services registered.</returns>
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