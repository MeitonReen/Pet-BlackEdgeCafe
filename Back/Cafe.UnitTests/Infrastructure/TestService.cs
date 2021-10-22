using Cafe.Infrastructure.ApplicationSettings.Root;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.UnitTests.Infrastructure
{
	public static class TestService
	{
		public static AppSettings GetAppSettings()
		{
			IConfiguration config = new ConfigurationBuilder()
				.SetBasePath(Environment.CurrentDirectory)
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
				.AddEnvironmentVariables()
				.AddUserSecrets<Program>()
				.Build();
			return config.Get<AppSettings>();
		}
	}
}
