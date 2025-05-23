using Infrastructure.Authentication.CustomEntities;

namespace Infrastructure.Authentication.Interfaces
{
	public interface IUserRepository
	{
		IEnumerable<AppUser> GetAll();
	}
}
