using Core.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Infrastructure.Shared.Services
{
	public class HttpContextProvider : IHttpContextProvider
	{
		private readonly IHttpContextAccessor httpContext;

		public HttpContextProvider(IHttpContextAccessor httpContext)
		{
			this.httpContext = httpContext;
		}
		public string? GetCurrentUserName()
		{
			if (httpContext is null)
				return null;

			var userName = httpContext.HttpContext.User.Identity?.Name;
			
			return userName;
		}

		public List<string>? CurrentUserRoles()
		{
			if (httpContext is null)
				return null;

			var roles = httpContext.HttpContext.User.Claims.Where(x=>x.Type == ClaimTypes.Role).Select(x=>x.Value);

			return roles.ToList();
		}
	}
}
