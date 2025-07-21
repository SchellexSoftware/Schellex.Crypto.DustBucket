using Schellex.Crypto.Coinbase.AdvancedApi.Configuration.Models;

namespace Schellex.Crypto.Coinbase.AdvancedApi.Configuration.Interfaces;

public interface IAppSettings
{
    CoinbaseSettings CoinbaseSettings { get; set; }
}
