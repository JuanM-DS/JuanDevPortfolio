using Core.Application.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Application
{
	public static class ServicesExtensions
	{
		public static IServiceCollection AddApplicationLayer(this IServiceCollection service)
		{
			return service;
		}
	}
}
