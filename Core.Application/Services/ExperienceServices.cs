using Core.Application.DTOs.Experience;
using Core.Application.Interfaces.Helpers;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
using Core.Application.Wrappers;
using Core.Domain.Entities;

namespace Core.Application.Services
{
	public class ExperienceServices : BaseServices<Experience, ExperienceDTO, SaveExperienceDTO>, IExperienceServices
    {
		private readonly ITechnologyItemRepository technologyItemRepo;

		public ExperienceServices(IExperienceRepository repo, IMapper mapper, ITechnologyItemRepository TechnologyItemRepo)
            : base(repo, mapper)
		{
			technologyItemRepo = TechnologyItemRepo;
		}

		public async Task<AppResponse<Empty>> AddTechnologyItemsAsync(Guid ExperienceId, List<string> itemsId)
		{
            var experiencTask = _repo.GetByIdAsync(ExperienceId);
			var TechnologyItemsTask = technologyItemRepo.GetAll();
		}
	}
}
