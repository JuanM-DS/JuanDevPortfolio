using Core.Application.QueryFilters;
using Core.Domain.Entities;

namespace Core.Application.Interfaces.Repositories
{
    public interface IWorkExperienceDetailRepository : IBaseRepository<WorkExperienceDetail>
    {
		public IEnumerable<WorkExperienceDetail> GetAll(WorkExperienceDetailFilter filter);
	}
}
