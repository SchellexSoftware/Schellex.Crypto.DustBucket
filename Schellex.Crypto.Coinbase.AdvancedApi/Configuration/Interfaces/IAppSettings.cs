using Schellex.Crypto.Coinbase.AdvancedApi.Configuration.Models;

namespace Schellex.Crypto.Coinbase.AdvancedApi.Configuration.Interfaces;

/// <summary>
/// Represents the abstraction for accessing application configuration values related to Coinbase API settings.
/// </summary>
public interface IAppSettings
{
    /// <summary>
    /// Gets or sets the Coinbase-specific configuration values such as API credentials and endpoint settings.
    /// </summary>  
    CoinbaseSettings CoinbaseSettings { get; set; }
}
