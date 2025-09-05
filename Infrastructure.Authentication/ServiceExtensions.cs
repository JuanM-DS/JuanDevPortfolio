using Core.Application.Interfaces.Services;
using Core.Application.Interfaces.Shared;
using Core.Application.Wrappers;
using Core.Domain.Entities;
using Infrastructure.Authentication.Context;
using Infrastructure.Authentication.Context.Interceptors;
using Infrastructure.Authentication.CustomEntities;
using Infrastructure.Authentication.Interfaces;
using Infrastructure.Authentication.Repositories;
using Infrastructure.Authentication.Seeds;
using Infrastructure.Authentication.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Infrastructure.Authentication
{
	public static class ServiceExtensions
    {
        public static IServiceCollection AddAuthenticationLayer(this IServiceCollection service, IConfiguration confi)
        {

            service.AddSingleton<AuditablePropertiesInterceptor>();
            service.AddDbContext<IdentityContext>((sp, options) =>
            {
                //var encryptationServices = sp.GetRequiredService<IEncryptationServices>();
                //var descripConnection = encryptationServices.Decrypt(confi.GetConnectionString("SqlConnectionString")!);
                var auditablePropertiesInterceptor = sp.GetRequiredService<AuditablePropertiesInterceptor>();

				options.UseSqlServer(
                    confi.GetConnectionString("SqlConnectionString"),
                    x => x.MigrationsAssembly(typeof(IdentityContext).Assembly)
                    );
                options.AddInterceptors(auditablePropertiesInterceptor);
			});

            service.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();

            service.AddScoped<IAccountServices, AccountServices>()
                .AddScoped<IUserServices, UserServices>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IRefreshTokenRepository, RefreshTokenRepository>()
                .AddScoped<ITokenServices, TokenServices>()
				.AddJwtConfigurations(confi);

            return service;
        }

		private static IServiceCollection AddJwtConfigurations(this IServiceCollection service, IConfiguration confi)
        {
            service.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(option =>
            {
                option.SaveToken = true;
                option.RequireHttpsMetadata = false;
                option.TokenValidationParameters = new TokenValidationParameters() 
                { 
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = confi.GetSection("JwtSettings")["Issuer"],
                    ValidAudience = confi.GetSection("JwtSettings")["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(confi.GetSection("JwtSettings")["SecretKey"]!)),
                    ClockSkew = TimeSpan.Zero
                };
                option.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = x =>
                    {
                        x.NoResult();
                        x.Response.ContentType = "application/json";
                        x.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        return x.Response.WriteAsJsonAsync(AppError.Create(x.Exception.Message).BuildResponse<Empty>(HttpStatusCode.InternalServerError));
                    },
					OnChallenge = (x) =>
					{
						x.HandleResponse();
						x.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
						x.Response.ContentType = "application/json";
						var response = new { Success = false, Error = "You are not authenticated" };
						return x.Response.WriteAsync(JsonConvert.SerializeObject(response));
					},
					OnForbidden = x =>
                    {
                        x.Response.ContentType = "application/json";
                        x.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                        return x.Response.WriteAsJsonAsync(AppError.Create("No estas authorizado para utilizar el recurso").BuildResponse<Empty>(HttpStatusCode.Unauthorized));
                    },
					OnTokenValidated = context =>
					{
						if (context.Principal?.Identity?.IsAuthenticated != true)
						{
							context.Fail("Token valid but not authenticated");
						}
						return Task.CompletedTask;
					}
				};
            });
               

            return service;
        }
	    
        public static async Task RegisterAuthenticationSeeds(this WebApplication app)
        {
            var scope = app.Services.CreateScope();
            var provider = scope.ServiceProvider;

			var roleManager = provider.GetRequiredService<RoleManager<AppRole>>();
			var userManager = provider.GetRequiredService<UserManager<AppUser>>();
			var imageRepository = provider.GetRequiredService<IImageRepository>();


			await RoleSeeds.RegisteRolesSeedsAsync(roleManager);
            await UserSeeds.RegisteAdminUserSeed(userManager, imageRepository);
        }
    }
}
