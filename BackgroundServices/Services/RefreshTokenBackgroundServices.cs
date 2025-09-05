using Core.Domain.Enumerables;
using Infrastructure.Authentication.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Infrastructure.BackgroundServices.Services
{
	public class RefreshTokenBackgroundServices : BackgroundService
	{
		private readonly IServiceScopeFactory scopeFactory;

		public RefreshTokenBackgroundServices(IServiceScopeFactory scopeFactory)
		{
			this.scopeFactory = scopeFactory;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			while (!stoppingToken.IsCancellationRequested)
			{
				try
				{
					using var scope = scopeFactory.CreateAsyncScope();
					var service = scope.ServiceProvider.GetRequiredService<ITokenServices>();
					var result = await service.DeleteInactiveRefreshTokensAsync();

					if (!result)
					{
						Log.ForContext(LoggerKeys.BackgroundServices.ToString(), true)
							.Error("Hubo un error ejecutando el servicio para eliminar los refresh token inactivos");
					}
					else
					{
						Log.ForContext(LoggerKeys.BackgroundServices.ToString(), true)
							.Information("Se eliminaron los refresh token inactivos correctamente");
					}

					await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
				}
				catch (Exception ex)
				{
					Log.ForContext(LoggerKeys.BackgroundServices.ToString(), true)
						.Error(ex, "Mientras se eliminaron los refresh token.");
				}
			}
		}
	}
}
