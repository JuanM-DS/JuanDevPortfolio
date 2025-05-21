using Core.Application.QueryFilters;
using Core.Domain.Entities;

namespace Core.Application.Interfaces.Repositories
{
    public interface IProjectRepository : IBaseRepository<Project>
    {
		public IEnumerable<Project> GetAll(ProjectFilter filter);
	}
}
