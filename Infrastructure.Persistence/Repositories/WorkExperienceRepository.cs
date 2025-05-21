using Core.Application.Interfaces.Repositories;
using Core.Application.QueryFilters;
using Core.Domain.Entities;
using Infrastructure.Persistence.Context;

namespace Infrastructure.Persistence.Repositories
{
    public class WorkExperienceRepository : BaseRepository<WorkExperience>, IWorkExperienceRepository
    {
        public WorkExperienceRepository(MainContext context)
            : base(context)
        { }

		public IEnumerable<WorkExperience> GetAll(WorkExperienceFilter filter)
		{
			var query = _entity.AsQueryable();

			if (!string.IsNullOrWhiteSpace(filter.Position))
				query = query.Where(x => x.Position.Contains(filter.Position));

			if (!string.IsNullOrWhiteSpace(filter.CompanyName))
				query = query.Where(x => x.CompanyName.Contains(filter.CompanyName));

			if (filter.FromDate is not null)
				query = query.Where(x => x.FromDate == filter.FromDate);

			if (filter.ToDate is not null)
				query = query.Where(x => x.ToDate == filter.ToDate);

			if (filter.ProfileId is not null)
				query = query.Where(x => x.ProfileId == filter.ProfileId);

			return query.AsEnumerable();
		}
	}
}
