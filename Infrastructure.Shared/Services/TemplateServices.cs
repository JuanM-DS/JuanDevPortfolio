using Core.Application.Interfaces.Helpers;
using RazorLight;

namespace Infrastructure.Shared.Services
{
	public class TemplateServices : ITemplateServices
	{
		private readonly IRazorLightEngine engine;

		public TemplateServices()
		{
			engine = new RazorLightEngineBuilder().UseFileSystemProject(Path.Combine(Directory.GetCurrentDirectory(), "Templates")).Build();
		}

		public async Task<string?> GetRazorTemplateAsStringAsync<TModel>(string ViewName, TModel model)
		{
			return await engine.CompileRenderAsync(ViewName, model);
		}

		public async Task<string?> GetRazorTemplateAsStringAsync(string ViewName)
		{
			var result = await engine.CompileTemplateAsync(ViewName);
			return result.ToString();
		}
	}
}
