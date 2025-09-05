using Asp.Versioning;
using Core.Application.Validations;
using Core.Domain.Enumerables;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Serilog;

namespace JuanDevPortfolio.Api.Extensions
{
	public static class ServicesExtensions
	{
		public static IServiceCollection  AddApiBehaviorExtensions(this IServiceCollection service)
		{
			service.AddControllers()
				.ConfigureApiBehaviorOptions(option =>
				{
					option.SuppressModelStateInvalidFilter = true;
				});
			return service;
		}

		public static IServiceCollection AddLogExtensions(this IServiceCollection service)
		{
			service.AddLogging();
			Log.Logger = new LoggerConfiguration()
				.MinimumLevel.Verbose()
				.WriteTo.File("Logs\\General_log.txt")
				.WriteTo.Logger(lg =>

					lg.Filter.ByIncludingOnly(f => f.Properties.ContainsKey(LoggerKeys.RepositoryLogs.ToString()))
					.WriteTo.File($"Logs\\Infrastructure\\{LoggerKeys.RepositoryLogs}.txt")
				)
				.WriteTo.Logger(lg =>
					lg.Filter.ByIncludingOnly(p => p.Properties.ContainsKey(LoggerKeys.AuthenticationLogs.ToString()))
					.WriteTo.File($"Logs\\Infrastructure\\{LoggerKeys.AuthenticationLogs}.txt")
				)
				.WriteTo.Logger(x =>
				{
					x.Filter.ByIncludingOnly(p => p.Properties.ContainsKey(LoggerKeys.SharedLogs.ToString()))
					.WriteTo.File($"Logs\\Infrastructure\\{LoggerKeys.SharedLogs}.txt");
				})
				.WriteTo.Logger(l =>
				{
					l.Filter.ByIncludingOnly(p => p.Properties.ContainsKey(LoggerKeys.BackgroundServices.ToString()))
					.WriteTo.File($"Logs\\Infrastructure\\{LoggerKeys.BackgroundServices}.txt");
				})
				.CreateLogger();
			return service;
		}

		public static IServiceCollection AddVersioningExtensions(this IServiceCollection service)
		{
			service.AddApiVersioning(option =>
			{
				option.DefaultApiVersion = new ApiVersion(1, 0);
				option.AssumeDefaultVersionWhenUnspecified = true;
				option.ReportApiVersions = true;
			});

			return service;
		}

		public static IServiceCollection AddValidationsExtensions(this IServiceCollection service)
		{
			service.AddMvc(x =>
			{
				x.Filters.Add<ValidationMiddleware>();
			});

			service.AddValidatorsFromAssemblyContaining<CommentReferencesValidations>();

			return service;
		}

		public static IServiceCollection AddSwaggerExtensions(this IServiceCollection  service)
		{
			service.AddSwaggerGen(option =>
			{
				var xmls = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly).ToList();
				xmls.ForEach(x => option.IncludeXmlComments(x));

				option.SwaggerDoc("v1", new OpenApiInfo 
				{ 
					Version = "1.0",
					Title = "JuanDevPortfolio",
					Description = "My api to maintenance my personal portfolio",
					Contact = new OpenApiContact
					{
						Name = "Juan De Los Santos",
						Email = "juanm.2004.sd@gmail.com",
						Url = new Uri("https://www.linkedin.com/in/juan-manuel-de-los-santos-069755250/")
					}
				});

				option.DescribeAllParametersInCamelCase();
				option.EnableAnnotations();

				option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Name = "Authorization",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.ApiKey,
					Scheme = JwtBearerDefaults.AuthenticationScheme,
					BearerFormat = "JWT",
					Description = "JWT Authorization header using the Bearer scheme.\n\nExample: \"Bearer {token}\""
				});

				option.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Name = "Bearer",
							In = ParameterLocation.Header,
							Scheme = JwtBearerDefaults.AuthenticationScheme,
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = JwtBearerDefaults.AuthenticationScheme
							} 
						},
						new List<string>()
					}
				});
			});

			return service;
		}
	}
}
