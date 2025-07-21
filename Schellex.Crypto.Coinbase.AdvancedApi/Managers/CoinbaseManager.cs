using Jose;
using System.Text;
using System.Text.Json;
using System.Security.Cryptography;
using Schellex.Crypto.Coinbase.AdvancedApi.Interfaces.Managers;
using Schellex.Crypto.Coinbase.AdvancedApi.Configuration.Interfaces;

namespace Schellex.Crypto.Coinbase.AdvancedApi.Managers
{
    public class CoinbaseManager : ICoinbaseManager
    {
        private readonly IAppSettings _appSettings;

        public CoinbaseManager(IAppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public async Task<string> CreateOrderAsync(string productId, decimal price, string side)
        {
            var endpoint = "brokerage/orders";

            var orderBody = new
            {
                client_order_id = Guid.NewGuid().ToString(),
                product_id = productId,
                side = side,
                order_configuration = new
                {
                    market_market_ioc = new
                    {
                        quote_size = price.ToString("F2"),
                        rfq_disabled = true
                    }
                },
            };

            var json = JsonSerializer.Serialize(orderBody);

            var result = await CallApiPOSTAsync(endpoint, json);
            return result;
        }

        public async Task<string> GetAccountInfoAsync()
        {
            string endpoint = "brokerage/accounts";
            var result = await CallApiGETAsync(endpoint);
            return result;
        }

        public async Task<string> GetCurrentPriceAsync(string productId = "BTC-USD")
        {
            string endpoint = $"brokerage/products/{productId}";
            var result = await CallApiGETAsync(endpoint);
            return result;
        }

        private string GetAuthToken(string endpoint, string action)
        {
            string name = _appSettings.CoinbaseSettings.ApiKey;
            string cbPrivateKey = _appSettings.CoinbaseSettings.Secret;

            string key = ParseKey(cbPrivateKey);
            string token = GenerateToken(name, key, $"{action} {endpoint}");

            return token;
        }

        private string GenerateToken(string name, string secret, string uri)
        {
            if (string.IsNullOrEmpty(secret))
            {
                throw new ArgumentException("Secret key cannot be null or empty.");
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Name cannot be null or empty.");
            }

            var privateKeyBytes = Convert.FromBase64String(secret); // Assuming PEM is base64 encoded
            using var key = ECDsa.Create();
            key.ImportECPrivateKey(privateKeyBytes, out _);

            var payload = new Dictionary<string, object>
                    {
                        { "sub", name },
                        { "iss", "coinbase-cloud" },
                        { "nbf", Convert.ToInt64((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds) },
                        { "exp", Convert.ToInt64((DateTime.UtcNow.AddMinutes(1) - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds) },
                        { "uri", uri }
                    };

            var extraHeaders = new Dictionary<string, object>
                    {
                        { "kid", name },
                        // add nonce to prevent replay attacks with a random 10 digit number
                        { "nonce", RandomHex(10) },
                        { "typ", "JWT"}
                    };

            var encodedToken = JWT.Encode(payload, key, JwsAlgorithm.ES256, extraHeaders);
            return encodedToken;
        }

        private string ParseKey(string key)
        {
            List<string> keyLines = new List<string>();
            keyLines.AddRange(key.Split('\n', StringSplitOptions.RemoveEmptyEntries));

            keyLines.RemoveAt(0);
            keyLines.RemoveAt(keyLines.Count - 1);

            return String.Join("", keyLines);
        }

        private string RandomHex(int digits)
        {
            byte[] buffer = new byte[digits / 2];
            Random random = new Random();
            random.NextBytes(buffer);

            string result = String.Concat(buffer.Select(x => x.ToString("X2")).ToArray());

            if (digits % 2 == 0)
            {
                return result;
            }

            return result + random.Next(16).ToString("X");
        }

        private async Task<string> CallApiGETAsync(string endpoint)
        {
            var bearerToken = GetAuthToken($"{_appSettings.CoinbaseSettings.BaseUrl}/{endpoint}", "GET");

            if (string.IsNullOrEmpty(bearerToken))
            {
                throw new Exception("Bearer token is null or empty. Please check your authentication settings.");
            }

            using (var client = new HttpClient())
            {
                using (var request = new HttpRequestMessage(HttpMethod.Get, $"https://{_appSettings.CoinbaseSettings.BaseUrl}/{endpoint}"))
                {
                    if (!string.IsNullOrEmpty(bearerToken))
                    {
                        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", bearerToken);
                    }

                    var response = client.Send(request);
                    if (response != null)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return result;
                    }
                    else
                    {
                        throw new Exception("Response is null. Please check your API endpoint and network connection.");
                    }
                }
            }
        }

        private async Task<string> CallApiPOSTAsync(string endpoint, string jsonPayload)
        {
            var bearerToken = GetAuthToken($"{_appSettings.CoinbaseSettings.BaseUrl}/{endpoint}", "POST");

            if (string.IsNullOrEmpty(bearerToken))
            {
                throw new Exception("Bearer token is null or empty. Please check your authentication settings.");
            }

            using (var client = new HttpClient())
            {
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                using (var request = new HttpRequestMessage(HttpMethod.Post, $"https://{_appSettings.CoinbaseSettings.BaseUrl}/{endpoint}") { Content = content })
                {
                    if (!string.IsNullOrEmpty(bearerToken))
                    {
                        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", bearerToken);
                    }

                    var response = client.Send(request);
                    if (response != null)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return result;
                    }
                    else
                    {
                        throw new Exception("Response is null. Please check your API endpoint and network connection.");
                    }
                }
            }
        }
    }
}