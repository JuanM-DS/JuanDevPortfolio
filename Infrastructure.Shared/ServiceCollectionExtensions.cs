using Microsoft.Extensions.DependencyInjection;
namespace Infrastructure.Shared
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddSharedLayer(this IServiceCollection service)
		{
			service.AddDataProtection();

			return service;
		}
	}
}
