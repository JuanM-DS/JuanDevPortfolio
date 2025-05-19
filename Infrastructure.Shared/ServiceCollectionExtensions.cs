using Core.Application.Interfaces.Helpers;
using Infrastructure.Shared.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace Infrastructure.Shared
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddSharedLayer(this IServiceCollection service, IConfiguration confi)
		{
			service.AddDataProtection();
			service.AddHttpContextAccessor();

			service.AddScoped<IEmailServices, EmailServices>();
			service.AddScoped<ITemplateServices, TemplateServices>();
			service.AddSingleton<IHttpContextProvider, HttpContextProvider>();
			return service;
		}
	}
}
