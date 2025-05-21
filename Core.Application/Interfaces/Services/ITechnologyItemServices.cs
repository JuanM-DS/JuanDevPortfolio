using Core.Application.DTOs.Skill;
using Core.Application.DTOs.TTechnologyItem;
using Core.Application.QueryFilters;
using Core.Application.Wrappers;
using Core.Domain.Entities;

namespace Core.Application.Interfaces.Services
{
    public interface ITechnologyItemServices : IBaseServices<TechnologyItem, TechnologyItemDTO, SaveTechnologyItemDTO>
    {
		public AppResponse<List<TechnologyItemDTO>> GetAll(TechnologyItemFilter filter);
	}
}
