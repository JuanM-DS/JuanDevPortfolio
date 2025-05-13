namespace Core.Application.Interfaces.Helpers
{
	public interface IHttpContextProvider
	{
		public string? GetCurrentUserName();

		public List<string>? CurrentUserRoles();
	}
}
