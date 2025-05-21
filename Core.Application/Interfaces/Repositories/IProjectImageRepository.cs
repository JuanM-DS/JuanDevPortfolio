using Core.Application.QueryFilters;
using Core.Domain.Entities;

namespace Core.Application.Interfaces.Repositories
{
    public interface IProjectImageRepository : IBaseRepository<ProjectImage>
    {
		public IEnumerable<ProjectImage> GetAll(ProjectImageFilter filter);
	}
}
