using Core.Application.Interfaces.Repositories;
using Core.Application.QueryFilters;
using Core.Domain.Entities;
using Infrastructure.Persistence.Context;

namespace Infrastructure.Persistence.Repositories
{
    public class ProjectImageRepository : BaseRepository<ProjectImage>, IProjectImageRepository
    {
        public ProjectImageRepository(MainContext context)
            : base(context)
        { }

		public IEnumerable<ProjectImage> GetAll(ProjectImageFilter filter)
		{
			var query = _entity.AsQueryable();

			if (filter.ProjectId is not null)
				query = query.Where(x => x.ProjectId == filter.ProjectId);

			return query.AsEnumerable();
		}
	}
}
