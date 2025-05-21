using Core.Application.QueryFilters;
using Core.Domain.Entities;

namespace Core.Application.Interfaces.Repositories
{
    public interface IProfileRepository : IBaseRepository<Profile>
    {
		public IEnumerable<Profile> GetAll(ProfileFilter filter);
	}
}
