using Microsoft.Extensions.Configuration;
using Schellex.Crypto.Coinbase.AdvancedApi.Configuration.Interfaces;

namespace Schellex.Crypto.Coinbase.AdvancedApi.Configuration.Models;

/// <summary>
/// Loads and holds configuration values from the appsettings files used by the application,
/// specifically the Coinbase API configuration.
/// </summary>
public record class AppSettings : IAppSettings
{

    /// <summary>
    /// Gets or sets the Coinbase API configuration values.
    /// </summary>
    public required CoinbaseSettings CoinbaseSettings { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="AppSettings"/> class by binding configuration values.
    /// </summary>
    /// <param name="configuration">The application's configuration interface used to bind settings.</param>
    /// <exception cref="Exception">Thrown when the CoinbaseSettings section is not found or is invalid.</exception>
    public AppSettings(IConfiguration configuration)
    {
        CoinbaseSettings = configuration.GetRequiredSection(nameof(CoinbaseSettings)).Get<CoinbaseSettings>() ?? throw new Exception($"{nameof(CoinbaseSettings)} configuration was not found.");
    }
}
