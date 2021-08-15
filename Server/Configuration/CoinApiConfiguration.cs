namespace StonksApplication.Server.Configuration
{
	public class AppConfiguration
	{
		public CoinApiConfiguration CoinApi { get; set; }
		public Auth0Configuration Auth0 { get; set; }
	}

	public class Auth0Configuration
	{
		public string Authority { get; set; }
		public string Audience { get; set; }
	}
	public class CoinApiConfiguration
	{
		public AuthConfiguration Auth { get; set; }
		public EndpointsConfiguration Endpoints { get; set; }
	}

	public class AuthConfiguration
	{
		public string Key { get; set; }
	}

	public class EndpointsConfiguration
	{
		public string FetchAssets { get; set; }
		public string FetchAssetIcons { get; set; }
		public string FetchExchanges { get; set; }
	}
}
