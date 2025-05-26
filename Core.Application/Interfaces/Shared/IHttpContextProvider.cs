namespace Core.Application.Interfaces.Shared
{
	public interface IHttpContextProvider
	{
		public Guid? GetCurrentUserId();

		public List<string>? CurrentUserRoles();
	}
}
