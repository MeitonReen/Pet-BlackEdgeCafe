using Cafe.Infrastructure.ApplicationSettings.Root;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace Cafe
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Log.Logger = new LoggerConfiguration()
				.MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
				.Enrich.FromLogContext()
				.WriteTo.Console(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] [{SourceContext:l}] {Message}{NewLine}{Exception}")
				.WriteTo.File("./logs/main.txt", outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] [{SourceContext:l}] {Message}{NewLine}{Exception}")
				.CreateBootstrapLogger();

			CreateHostBuilder(args).ConfigureAppConfiguration(conf =>
				conf.AddUserSecrets<Program>()).Build().Run();
		}
		public static IHostBuilder CreateHostBuilder(string[] args) =>
			 Host.CreateDefaultBuilder(args)
				.UseSerilog((context, services, configuration) =>
				{
					AppSettings appSettings = context.Configuration.Get<AppSettings>();
					string userIdConst = appSettings.Constants.LoggingParams
						.UserIdentifier;

					configuration
						.ReadFrom.Configuration(context.Configuration)
						.Enrich.FromLogContext()
						.WriteTo.Console(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] [{SourceContext:l}] {Message}{NewLine}{Exception}")
						.WriteTo.Map(userIdConst, (userNameWithIdFromLogContext, conf) =>
							conf.File($"./logs/{userNameWithIdFromLogContext}.txt", outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] [{SourceContext:l}] {Message}{NewLine}{Exception}"))
						.WriteTo.Map(userIdConst, (userIdLogContext, conf) =>
							conf.File("./logs/main.txt", shared: true, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] [{SourceContext:l}] save to --> [{" + userIdConst + "}.txt]{NewLine}"))
						.WriteTo.Logger(conf =>
							conf.WriteTo.File("./logs/main.txt", shared: true, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] [{SourceContext:l}] {Message}{NewLine}{Exception}")
								.Filter.ByExcluding(logEvent =>
									logEvent.Properties.ContainsKey(userIdConst)));
				})
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder
						.UseStartup<Startup>();
				});
	}
}
