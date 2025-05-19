using Infrastructure.Authentication.Context;
using Infrastructure.Authentication.CustomEntities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace Infrastructure.Authentication
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddAuthenticationLayer(this IServiceCollection service, IConfiguration confi)
        {
            service.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();

            service.AddJwtConfigurations(confi);

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
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(confi.GetSection("JwtSettings")["ScretKey"]!)),
                    ClockSkew = TimeSpan.Zero
                };
            });
               

            return service;
        }
	}
}
