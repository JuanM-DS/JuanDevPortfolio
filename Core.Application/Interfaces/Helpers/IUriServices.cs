namespace Core.Application.Interfaces.Helpers
{
	public interface IUriServices
	{
		string GetURL(string route, Dictionary<string, string> parameters);
	}
}
