using Core.Application.QueryFilters;
using Core.Domain.Entities;

namespace Core.Application.Interfaces.Repositories
{
    public interface IWorkExperienceRepository : IBaseRepository<WorkExperience>
    {
		public IEnumerable<WorkExperience> GetAll(WorkExperienceFilter filter);
	}
}
