using Infrastructure.Authentication.CustomEntities;
using System.Linq.Expressions;

namespace Infrastructure.Authentication.Interfaces
{
	public interface IUserRepository
	{
		IEnumerable<AppUser> GetAll();
		IEnumerable<AppUser> GetAllWithInclude(params Expression<Func<AppUser, object>>[] parameters);
	}
}
