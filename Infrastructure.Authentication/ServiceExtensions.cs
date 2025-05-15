using Infrastructure.Authentication.Context;
using Infrastructure.Authentication.CustomEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Authentication
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddAuthenticationLayer(this IServiceCollection service)
        {
            service.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();

            return service;
        }
    }
}
