using Core.Application.DTOs.Skill;
using Core.Application.QueryFilters;
using Core.Application.Wrappers;
using Core.Domain.Entities;

namespace Core.Application.Interfaces.Services
{
	public interface ISkillServices : IBaseServices<Skill, SkillDTO, SaveSkillDTO>
    {
		public Task<AppResponse<Empty>> AddTechnologyItemsAsync(Guid SkillId, List<Guid> itemsId);
		public AppResponse<List<SkillDTO>> GetAll(SkillFilter filter);
	}
}
