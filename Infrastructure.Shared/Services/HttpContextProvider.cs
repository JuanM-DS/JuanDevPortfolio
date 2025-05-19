using Core.Application.Interfaces.Helpers;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Infrastructure.Shared.Services
{
	public class HttpContextProvider : IHttpContextProvider
	{
		private readonly IHttpContextAccessor accesor;

		public HttpContextProvider(IHttpContextAccessor accesor)
		{
			this.accesor = accesor;
		}
		public string? GetCurrentUserName()
		{
			if (accesor is null)
				return null;

			var userName = accesor.HttpContext?.User.Identity?.Name;
			
			return userName;
		}

		public List<string>? CurrentUserRoles()
		{
			if (accesor is null)
				return null;

			var roles = accesor.HttpContext?.User.Claims.Where(x=>x.Type == ClaimTypes.Role).Select(x=>x.Value);

			return roles?.ToList();
		}
	}
}
