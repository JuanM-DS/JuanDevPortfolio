using Core.Application.QueryFilters;
using Core.Domain.Entities;

namespace Core.Application.Interfaces.Repositories
{
    public interface ISkillRepository : IBaseRepository<Skill>
    {
		public IEnumerable<Skill> GetAll(SkillFilter filter);
	}
}
