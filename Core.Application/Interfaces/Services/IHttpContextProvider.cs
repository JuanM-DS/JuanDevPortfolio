namespace Core.Application.Interfaces.Services
{
	public interface IHttpContextProvider
	{
		public string? GetCurrentUserName();

		public List<string>? CurrentUserRoles();
	}
}
