using Core.Application.Interfaces.Services;
using Core.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Application
{
	public static class ServicesExtensions
	{
		public static IServiceCollection AddApplicationLayer(this IServiceCollection service)
		{
            #region DI
            service.AddScoped<ICommentReferencesServices, CommentReferencesServices>();
            service.AddScoped<IExperienceServices, ExperienceServices>();
            service.AddScoped<IExperienceDetailServices, ExperienceDetailServices>();
            service.AddScoped<IProfileServices, ProfileServices>();
            service.AddScoped<IProjectServices, ProjectServices>();
            service.AddScoped<IProjectImageServices, ProjectImageServices>();
            service.AddScoped<ISkillServices, SkillServices>();
            service.AddScoped<ITechnologyItemServices, TechnologyItemServices>();
            #endregion
            return service;
		}
	}
}
