namespace Core.Application.Interfaces.Shared
{
	public interface ITemplateServices
	{
		Task<string?> GetRazorTemplateAsStringAsync<TModel>(string ViewName, TModel model);
		Task<string?> GetRazorTemplateAsStringAsync(string ViewName);

	}
}
