using Core.Application.Interfaces.Helpers;
using Infrastructure.Shared.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Mail;
namespace Infrastructure.Shared
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddSharedLayer(this IServiceCollection service, IConfiguration confi)
		{
			service.AddDataProtection();
			service.AddHttpContextAccessor();

			service.AddFluentEmail(confi.GetSection("EmailSmpSettings")["SmptFrom"])
				.AddRazorRenderer()
				.AddSmtpSender(new SmtpClient()
				{
					Host = confi.GetSection("EmailSmpSettings")["SmptHost"]!,
					Port = int.TryParse(confi.GetSection("EmailSmpSettings")["SmptPort"], out var result) ? result : 875
				});

			service.AddSingleton<IHttpContextProvider, HttpContextProvider>();
			return service;
		}
	}
}
