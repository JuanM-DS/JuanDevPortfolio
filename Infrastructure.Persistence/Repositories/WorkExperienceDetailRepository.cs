using Core.Application.Interfaces.Repositories;
using Core.Application.QueryFilters;
using Core.Domain.Entities;
using Infrastructure.Persistence.Context;

namespace Infrastructure.Persistence.Repositories
{
    public class WorkExperienceDetailRepository : BaseRepository<WorkExperienceDetail>, IWorkExperienceDetailRepository
    {
        public WorkExperienceDetailRepository(MainContext context)
            : base(context)
        { }

		public IEnumerable<WorkExperienceDetail> GetAll(WorkExperienceDetailFilter filter)
		{
			var query = _entity.AsQueryable();

			if (filter.ExperienceId is not null)
				query = query.Where(x => x.ExperienceId == filter.ExperienceId);

			if (!string.IsNullOrWhiteSpace(filter.Title))
				query = query.Where(x => x.Title.Contains(filter.Title));

			return query.AsEnumerable();
		}
	}
}
