using Core.Application.Interfaces.Services;
using Infrastructure.Shared.Services;
using Microsoft.Extensions.DependencyInjection;
namespace Infrastructure.Shared
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddSharedLayer(this IServiceCollection service)
		{
			service.AddDataProtection();
			service.AddHttpContextAccessor();
			service.AddSingleton<IHttpContextProvider, HttpContextProvider>();
			return service;
		}
	}
}
