using Schellex.Crypto.Coinbase.AdvancedApi.Configuration.Interfaces;
using Schellex.Crypto.Coinbase.AdvancedApi.Interfaces.Services;

namespace Schellex.Crypto.Coinbase.AdvancedApi.Services;

/// <summary>
/// Provides functionality to retrieve account information from the Coinbase Advanced API.
/// </summary>
public class AccountService : ServiceBase, IAccountService
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AccountService"/> class.
    /// </summary>
    /// <param name="appSettings">Injected application settings used for API configuration and authentication.</param>
    public AccountService(IAppSettings appSettings) : base(appSettings) { }

    /// <summary>
    /// Retrieves the authenticated account's information from the Coinbase Advanced API.
    /// </summary>
    /// <returns>A task that returns a JSON string containing account details.</returns>
    public async Task<string> GetAccountInfoAsync()
    {
        string endpoint = "brokerage/accounts";
        var result = await CallApiGETAsync(endpoint);
        return result;
    }
}
