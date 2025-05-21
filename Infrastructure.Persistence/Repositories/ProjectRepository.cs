using Core.Application.Interfaces.Repositories;
using Core.Application.QueryFilters;
using Core.Domain.Entities;
using Infrastructure.Persistence.Context;

namespace Infrastructure.Persistence.Repositories
{
    public class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        public ProjectRepository(MainContext context)
            : base(context)
        { }

		public IEnumerable<Project> GetAll(ProjectFilter filter)
		{
			var query = _entity.AsQueryable();

			if (!string.IsNullOrWhiteSpace(filter.Title))
				query = query.Where(x => x.Title.Contains(filter.Title));

			if (filter.ProfileId is not null)
				query = query.Where(x => x.ProfileId == filter.ProfileId);

			return query.AsEnumerable();
		}
	}
}
