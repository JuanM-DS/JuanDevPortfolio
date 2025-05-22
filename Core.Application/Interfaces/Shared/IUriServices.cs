namespace Core.Application.Interfaces.Shared
{
	public interface IUriServices
	{
		string GetURL(string route, Dictionary<string, string> parameters);
	}
}
