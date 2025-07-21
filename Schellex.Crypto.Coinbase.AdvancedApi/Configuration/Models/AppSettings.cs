using Microsoft.Extensions.Configuration;
using Schellex.Crypto.Coinbase.AdvancedApi.Configuration.Interfaces;

namespace Schellex.Crypto.Coinbase.AdvancedApi.Configuration.Models;

public record class AppSettings : IAppSettings
{
    public required CoinbaseSettings CoinbaseSettings { get; set; }

    public AppSettings(IConfiguration configuration)
    {
        CoinbaseSettings = configuration.GetRequiredSection(nameof(CoinbaseSettings)).Get<CoinbaseSettings>() ?? throw new Exception($"{nameof(CoinbaseSettings)} configuration was not found.");
    }
}
