using Core.Application.Interfaces.Helpers;
using Core.Application.Interfaces.Repositories;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Context.Interceptors;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence
{
	public static class ServicesExtensions
	{
		public static IServiceCollection AddPersistenceLayer(this IServiceCollection service, IConfiguration confi)
		{
            #region context
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

			#endregion
			
			#region ID
			service.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            service.AddScoped<IExperienceRepository, ExperienceRepository>();
            service.AddScoped<IExperienceDetailRepository, ExperienceDetailRepository>();
            service.AddScoped<IProfileRepository, ProfileRepository>();
            service.AddScoped<IProjectRepository, ProjectRepository>();
            service.AddScoped<IProjectImageRepository, ProjectImageRepository>();
            service.AddScoped<ISkillRepository, SkillRepository>();
            service.AddScoped<ITechnologyItemRepository, TechnologyItemRepository>();
            service.AddScoped<ICommentReferencesRepository, CommentReferencesRepository>();
            #endregion

            return service;
		}
	}
}
