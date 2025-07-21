namespace Schellex.Crypto.Coinbase.AdvancedApi.Configuration.Models;

public record CoinbaseSettings
{
    public required string BaseUrl { get; set; }
    public required string ApiKey { get; set; }
    public required string Secret { get; set; }
    public required string PortfolioId { get; set; }
}
