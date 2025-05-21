using Core.Application.DTOs.Experience;
using Core.Application.QueryFilters;
using Core.Application.Wrappers;
using Core.Domain.Entities;

namespace Core.Application.Interfaces.Services
{
	public interface IWorkExperienceServices : IBaseServices<WorkExperience, WorkExperienceDTO, SaveWorkExperienceDTO>
    {
        public Task<AppResponse<Empty>> AddTechnologyItemsAsync(Guid ExperienceId, List<Guid> itemsId);

		public AppResponse<List<WorkExperienceDTO>> GetAll(WorkExperienceFilter filter);
	}
}
