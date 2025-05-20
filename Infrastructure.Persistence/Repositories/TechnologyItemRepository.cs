using Core.Application.Interfaces.Repositories;
using Core.Application.QueryFilters;
using Core.Domain.Entities;
using Infrastructure.Persistence.Context;

namespace Infrastructure.Persistence.Repositories
{
	public class TechnologyItemRepository : BaseRepository<TechnologyItem>, ITechnologyItemRepository
    {
        public TechnologyItemRepository(MainContext context)
            : base(context)
        { }

		public IEnumerable<TechnologyItem> GetAll(TechnologyItemFilter filter)
		{
			var query = _entity.AsQueryable();

			if (!string.IsNullOrEmpty(filter.Title))
				query = query.Where(x => x.Title == filter.Title);

			if (filter.LevelType is not null)
				query = query.Where(x => x.LevelType == filter.LevelType);

			if (filter.Ids is not null && filter.Ids.Any())
				query = query.Where(x => filter.Ids.Contains(x.Id));

			return query.AsEnumerable();
		}
	}
}
