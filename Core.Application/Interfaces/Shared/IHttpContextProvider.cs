namespace Core.Application.Interfaces.Shared
{
	public interface IHttpContextProvider
	{
		public string? GetCurrentUserName();

		public List<string>? CurrentUserRoles();
	}
}
