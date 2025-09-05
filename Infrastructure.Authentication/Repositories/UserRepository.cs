using Infrastructure.Authentication.Context;
using Infrastructure.Authentication.CustomEntities;
using Infrastructure.Authentication.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

		public IEnumerable<AppUser> GetAllWithInclude(params Expression<Func<AppUser, object>>[] parameters)
		{
			var query = context.Users.AsQueryable();

			foreach (var item in parameters)
			{
				query = query.Include(item);
			}

			return query.AsEnumerable();
		}
	}
}
