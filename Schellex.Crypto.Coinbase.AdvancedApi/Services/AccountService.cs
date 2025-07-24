using Schellex.Crypto.Coinbase.AdvancedApi.Configuration.Interfaces;
using Schellex.Crypto.Coinbase.AdvancedApi.Interfaces.Services;

namespace Schellex.Crypto.Coinbase.AdvancedApi.Services;

public class AccountService : ServiceBase, IAccountService
{
    public AccountService(IAppSettings appSettings) : base(appSettings) { }

    public async Task<string> GetAccountInfoAsync()
    {
        string endpoint = "brokerage/accounts";
        var result = await CallApiGETAsync(endpoint);
        return result;
    }
}
