using Core.Application.Interfaces.Shared;
using RazorLight;

namespace Infrastructure.Shared.Services
{
	public class TemplateServices : ITemplateServices
	{
		private readonly IRazorLightEngine _engine;
		public TemplateServices()
		{
			_engine = new RazorLightEngineBuilder().UseFileSystemProject(Path.Combine(Directory.GetCurrentDirectory(), "Templates")).Build();
		}
		public async Task<string?> GetRazorTemplateAsStringAsync<TModel>(string ViewName, TModel model)
		{
			return await _engine.CompileRenderAsync(ViewName, model);
		}

		public async Task<string?> GetRazorTemplateAsStringAsync(string ViewName)
		{
			var result = await _engine.CompileTemplateAsync(ViewName);
			return result.ToString();
		}
	}
}
