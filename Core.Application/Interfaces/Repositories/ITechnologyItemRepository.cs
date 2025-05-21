using Core.Application.QueryFilters;
using Core.Domain.Entities;

namespace Core.Application.Interfaces.Repositories
{
    public interface ITechnologyItemRepository : IBaseRepository<TechnologyItem>
    {
		public IEnumerable<TechnologyItem> GetAll(TechnologyItemFilter filter);
	}
}
