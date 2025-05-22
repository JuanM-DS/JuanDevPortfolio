using Asp.Versioning;
using Core.Domain.Enumerables;
using Serilog;

namespace JuanDevPortfolio.Api.Extensions
{
	public static class ServicesExtensions
	{
		public static IServiceCollection AddLogExtensions(this IServiceCollection service)
		{
			service.AddLogging();
			Log.Logger = new LoggerConfiguration()
				.MinimumLevel.Information()
				.WriteTo.File("Logs\\General_log.txt")
				.WriteTo.Logger(lg =>

					lg.Filter.ByIncludingOnly(f => f.Properties.ContainsKey(LoggerKeys.RepositoryLogs.ToString()))
					.WriteTo.File($"Logs\\Infrastructure\\{LoggerKeys.RepositoryLogs}.txt")
				)
				.WriteTo.Logger(lg =>
					lg.Filter.ByIncludingOnly(p => p.Properties.ContainsKey(LoggerKeys.AuthenticationLogs.ToString()))
					.WriteTo.File($"Logs\\Infrastructure\\{LoggerKeys.AuthenticationLogs}.txt")
				)
				.WriteTo.Logger(x =>
				{
					x.Filter.ByIncludingOnly(p => p.Properties.ContainsKey(LoggerKeys.SharedLogs.ToString()))
					.WriteTo.File($"Logs\\Infrastructure\\{LoggerKeys.SharedLogs}.txt");
				})
				.CreateLogger();
			return service;
		}

		public static IServiceCollection AddVersioningExtensions(this IServiceCollection service)
		{
			service.AddApiVersioning(option =>
			{
				option.DefaultApiVersion = new ApiVersion(1, 0);
				option.AssumeDefaultVersionWhenUnspecified = true;
				option.ReportApiVersions = true;
			});

			return service;
		}
	}
}
