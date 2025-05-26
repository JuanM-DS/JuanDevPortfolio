using Core.Application.Interfaces.Shared;
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
		public Guid? GetCurrentUserId()
		{
			if (accesor is null)
				return null;

			var idClaim = accesor.HttpContext?.User.Claims.FirstOrDefault(x=>x.Type == "UserId");
			if (string.IsNullOrEmpty(idClaim?.Value))
				return null;

			return Guid.Parse(idClaim.Value);
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
