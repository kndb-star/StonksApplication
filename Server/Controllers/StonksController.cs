using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StonksApplication.Server.ThirdPartyIntegrations;
using StonksApplication.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StonksApplication.Server.Controllers
{
	[ApiController]
	[Route("v1/stonks")]
	[Authorize]
	public class StonksController : ControllerBase
	{
		private readonly ICoinApiProvider _coinApiProvider;
		public StonksController(ICoinApiProvider coinApiProvider)
		{
			_coinApiProvider = coinApiProvider;
		}

		[HttpGet("crypto")]
		public async Task<IEnumerable<Asset>> GetAllCryptos()
		{
			return await _coinApiProvider.GetAssets();
		}

		[HttpGet("exhanges")]
		public async Task<List<Exchange>> GetAllExchanges()
		{
			return await _coinApiProvider.GetExchanges();
		}
	}
}
