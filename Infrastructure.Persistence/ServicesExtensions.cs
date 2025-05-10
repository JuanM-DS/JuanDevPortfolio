using Application.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence
{
	public static class ServicesExtensions
	{
		public static IServiceCollection AddPersistenceLayer(this IServiceCollection service, IConfiguration confi, IServiceProvider sp)
		{
			var scoped = sp.CreateAsyncScope();
			var EncryptationServices = scoped.ServiceProvider.GetRequiredService<IEncryptationServices>();


			return service;
		}
	}
}
