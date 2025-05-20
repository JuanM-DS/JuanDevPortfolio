using Core.Application.DTOs.Experience;
using Core.Application.Wrappers;
using Core.Domain.Entities;

namespace Core.Application.Interfaces.Services
{
	public interface IExperienceServices : IBaseServices<Experience, ExperienceDTO, SaveExperienceDTO>
    {
        public Task<AppResponse<Empty>> AddTechnologyItemsAsync(Guid ExperienceId, List<Guid> itemsId);
    }
}
