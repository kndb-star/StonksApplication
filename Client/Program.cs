using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace StonksApplication.Client
{

	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);

			builder.RootComponents.Add<App>("#app");

			builder.Services.AddHttpClient("WebAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
				.AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

			builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
			  .CreateClient("WebAPI"));

			builder.Services.AddOidcAuthentication(options =>
			{
				builder.Configuration.Bind("Auth0", options.ProviderOptions);

				options.ProviderOptions.ResponseType = "code";
			});

			await builder.Build().RunAsync();
		}
	}
}
