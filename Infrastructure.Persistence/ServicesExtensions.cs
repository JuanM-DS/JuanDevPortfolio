using Application.Interfaces.Services;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Context.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence
{
	public static class ServicesExtensions
	{
		public static IServiceCollection AddPersistenceLayer(this IServiceCollection service, IConfiguration confi)
		{
			var connSrt = confi.GetConnectionString("SqlConnectionString");
			if (string.IsNullOrEmpty(connSrt))
				throw new Exception("La cadena de conexcion no fue encontra");

			service.AddSingleton<SaveAuditablePropertiesInterceptor>();

			service.AddDbContext<MainContext>((sp, option) =>
			{
				var encryptationServices = sp.GetRequiredService<IEncryptationServices>();
				var savingChangesInterceptor = sp.GetRequiredService<SaveAuditablePropertiesInterceptor>();

				var descrypConnSrt = encryptationServices.Encrypt(connSrt);
				
				option.UseSqlServer(descrypConnSrt, x => x.MigrationsAssembly(typeof(MainContext).Assembly));
				option.AddInterceptors(savingChangesInterceptor);
			});

			return service;
		}
	}
}
