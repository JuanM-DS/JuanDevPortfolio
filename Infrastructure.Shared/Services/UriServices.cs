using Core.Application.Interfaces.Shared;
using Microsoft.AspNetCore.WebUtilities;

namespace Infrastructure.Shared.Services
{
	public class UriServices : IUriServices
	{
		private readonly string host;

		public UriServices(string host)
		{
			this.host = host;
		}

		public string GetURL(string route, Dictionary<string, string> parameters)
		{
			var finalUrl = new Uri(Path.Combine(host, route)).ToString();

			foreach (var item in parameters)
			{
				finalUrl = QueryHelpers.AddQueryString(finalUrl, item.Key, item.Value);
			}

			return finalUrl;
		}
	}
}
