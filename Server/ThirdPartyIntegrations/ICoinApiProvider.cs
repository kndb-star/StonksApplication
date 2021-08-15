using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using StonksApplication.Server.Configuration;
using System;
using StonksApplication.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using System.Linq;
using System.Collections.Concurrent;

namespace StonksApplication.Server.ThirdPartyIntegrations
{
	public interface ICoinApiProvider
	{
		Task<IEnumerable<Asset>> GetAssets();
		Task<List<Exchange>> GetExchanges();
	}

	public class CoinApiProvider : ICoinApiProvider
	{
		private readonly AppConfiguration _appConfig;
		private readonly ILogger<CoinApiProvider> _logger;

		private readonly IMemoryCache _cache;
		public CoinApiProvider(IOptionsSnapshot<AppConfiguration> configuration, ILogger<CoinApiProvider> logger, IMemoryCache cache)
		{
			_appConfig = configuration.Value;
			_logger = logger;
			_cache = cache;
		}

		public async Task<IEnumerable<Asset>> GetAssets()
		{
			string key = "Assets";

			if (!_cache.TryGetValue<List<Asset>>(key, out List<Asset> data))
			{
				var assets = ExecuteRequestAsync<ConcurrentBag<Asset>>(_appConfig.CoinApi.Endpoints.FetchAssets, Method.GET);
				var assetIcons = ExecuteRequestAsync<ConcurrentBag<Asset>>(_appConfig.CoinApi.Endpoints.FetchAssetIcons, Method.GET);

				var finalResult = await Task.WhenAll(assets, assetIcons);

				var assetResults = assets.Result;
				var assetIconResults = assetIcons.Result;

				Parallel.ForEach(assetResults, (x) =>
				{
					x.url = assetIcons.Result.FirstOrDefault(y => y.asset_id == x.asset_id)?.url;
				});

				_cache.Set<IEnumerable<Asset>>(key, assetResults.Where(x => !string.IsNullOrWhiteSpace(x.url)).OrderBy(x => x.asset_id), DateTimeOffset.Now.AddMinutes(30));
			}
			return _cache.Get<IEnumerable<Asset>>(key);
		}
		public async Task<List<Exchange>> GetExchanges()
		{
			string key = "Exchanges";

			var cachedData = await _cache.GetOrCreateAsync<List<Exchange>>(key, (e) =>
			{
				_logger.LogInformation("Cache miss. Fetching data from CoinAPI.");
				e.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(30);
				return ExecuteRequestAsync<List<Exchange>>(_appConfig.CoinApi.Endpoints.FetchExchanges, Method.GET);
			});

			_logger.LogInformation($"Returning a total of {cachedData.Count} exchanges.");

			return cachedData;
		}


		private async Task<T> ExecuteRequestAsync<T>(string apiEndpoint, Method httpMethod)
		{
			// coinapi throttles
			var client = new RestClient(apiEndpoint);
			var request = new RestRequest(httpMethod);
			request.AddHeader("X-CoinAPI-Key", _appConfig.CoinApi.Auth.Key);
			IRestResponse response = await client.ExecuteAsync(request);

			if (!response.IsSuccessful)
			{
				_logger.LogError($"{httpMethod} call to {apiEndpoint} failed. Error message from CoinAPI: {response.ErrorMessage}");

				throw new ApplicationException("Failed to fetch data from CoinAPI. See logs for additional information.");
			}

			return JsonConvert.DeserializeObject<T>(response.Content);
		}
	}
}
