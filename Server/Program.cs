using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

namespace StonksApplication.Server
{
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.ConfigureAppConfiguration((configBuilder) =>
					{
						string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
						string configPath = Path.Combine(baseDirectory, "Configuration", "appsettings.hackathon.json");
						configBuilder.AddJsonFile(configPath, optional: false, reloadOnChange: true);
						
					});
					webBuilder.UseStartup<Startup>();
				});
	}
}
