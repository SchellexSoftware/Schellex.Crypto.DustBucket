# Dust Bucket

**Dust Bucket** is a standalone C# program that automatically buys cryptocurrency (e.g. BTC) using the Coinbase Advanced Trade API at scheduled intervals. It's designed to be run via a system scheduler (like Windows Task Scheduler or `cron`) to accumulate crypto passively with small daily purchases — often referred to as "buying dust."

---

## 🚀 Features

- 💰 Purchases a specified crypto using your chosen currency. `Example: BTC-USDC`
- 🔒 API authentication with JWT via ECDSA (ES256)
- ⏱️ Designed for unattended execution via scheduled task
- 📦 Lightweight, no third-party services required
- ⚙️ Uses [Coinbase Advanced Trade API](https://docs.cdp.coinbase.com/api-reference/advanced-trade-api/rest-api/orders/create-order)

---

## 🔧 Setup

1. **Clone the repo:**

Create `appsettings.local.json: # DO NOT COMMIT THIS FILE`

Fill in:

``` json
{
  "CoinbaseSettings": {
    "ApiKey": "your-api-key",
    "Secret": "your-base64-private-key",
    "BaseUrl": "api.coinbase.com",
    "PortfolioId": "default"
  }
}
```

⸻

🗓️ Automation

- Linux: Use cron
- Windows: Use Task Scheduler
- Mac: Use Automator + Calendar

⸻

⚠️ Security

- Never commit `appsettings.local.json` to Git
- Use user secrets or environment variables in production
- Limit API key permissions