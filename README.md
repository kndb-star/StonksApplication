# StonksApplication

The following needs to be configured before running the app

Client > wwwroot > appsettings.json
```json
{
	"Auth0": {
		"Authority": "",
		"ClientId": ""
	}
}

```

Server > configuration > appsettings.hackathon.json
```json
{
	"Auth0": {
		"Authority": "",
		"Audience": ""
	},
	"CoinApi": {
		"Auth": {
			"Key": ""
		},
		"Endpoints": {
			"FetchAssets": "https://rest.coinapi.io/v1/assets",
			"FetchAssetIcons": "https://rest.coinapi.io/v1/assets/icons/32",
			"FetchExchanges": "https://rest.coinapi.io/v1/exchanges"
		}
	}
}

```
