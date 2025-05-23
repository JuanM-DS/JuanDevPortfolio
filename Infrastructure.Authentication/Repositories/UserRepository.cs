using Infrastructure.Authentication.Context;
using Infrastructure.Authentication.CustomEntities;
using Infrastructure.Authentication.Interfaces;

namespace Infrastructure.Authentication.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly IdentityContext context;

		public UserRepository(IdentityContext context)
		{
			this.context = context;
		}

		public IEnumerable<AppUser> GetAll()
		{
			return context.Users.AsEnumerable();
		}
	}
}
