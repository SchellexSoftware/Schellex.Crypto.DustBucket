namespace Schellex.Crypto.Coinbase.AdvancedApi.Interfaces.Services;

public interface IAccountService
{
    Task<string> GetAccountInfoAsync();
}
