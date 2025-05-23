using Swashbuckle.AspNetCore.SwaggerUI;

namespace JuanDevPortfolio.Api.Extensions
{
	public static class AppExtensions
	{
		public static WebApplication UseSwaggerExtencions(this WebApplication app)
		{
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI(option =>
				{
					option.SwaggerEndpoint("/swagger/v1/swagger.json", "JuanDevPortFolio");
					option.DefaultModelRendering(ModelRendering.Model);
				});
			}

			return app;
		}
	}
}
