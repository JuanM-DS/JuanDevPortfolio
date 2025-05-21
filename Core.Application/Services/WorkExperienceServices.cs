using Core.Application.DTOs.Experience;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
using Core.Application.Mappings;
using Core.Application.QueryFilters;
using Core.Application.Wrappers;
using Core.Domain.Entities;
using System.Net;

namespace Core.Application.Services
{
	public class WorkExperienceServices : BaseServices<WorkExperience, WorkExperienceDTO, SaveWorkExperienceDTO>, IWorkExperienceServices
    {
		private readonly IWorkExperienceRepository repo;
		private readonly ITechnologyItemRepository technologyItemRepo;

		public WorkExperienceServices(IWorkExperienceRepository repo, ITechnologyItemRepository TechnologyItemRepo)
            : base(repo)
		{
			this.repo = repo;
			technologyItemRepo = TechnologyItemRepo;
		}

		public async Task<AppResponse<Empty>> AddTechnologyItemsAsync(Guid ExperienceId, List<Guid> itemsId)
		{
            var experiencTask = _repo.GetByIdAsync(ExperienceId, x => x.TechnologyItems);
			var TechnologyItems = technologyItemRepo.GetAll(new TechnologyItemFilter() { Ids = itemsId }).ToList();
			var experience = await experiencTask;

			if(experience is null)
				AppError.Create("No se encontró ninguna experiencia laboral con el Id enviado")
					.BuildResponse<Empty>(HttpStatusCode.BadRequest)
					.Throw();

			if (TechnologyItems.Any())
				AppError.Create("No se encontró ningún Ítem tecnológico con los Ids enviado")
					.BuildResponse<Empty>(HttpStatusCode.BadRequest)
					.Throw();

			experience!.TechnologyItems.ToList().AddRange(TechnologyItems);
			var result = await _repo.UpdateAsync(experience);
			if (result)
				AppError.Create("Hubo un problema al registrar los Ítem")
					.BuildResponse<Empty>(HttpStatusCode.BadRequest)
					.Throw();

			return new(HttpStatusCode.OK);
		}

		public AppResponse<List<WorkExperienceDTO>> GetAll(WorkExperienceFilter filter)
		{
			var data = repo.GetAll(filter).ToList();
			if (data is null || !data.Any())
				return new(HttpStatusCode.NoContent, "No hay elementos para mostrar");

			var dataDto = Mapper.Map<WorkExperienceDTO, WorkExperience>(data);
			if (dataDto is null)
				AppError.Create("Hubo problemas al mappear la request")
					.BuildResponse<WorkExperienceDTO>(HttpStatusCode.InternalServerError)
					.Throw();

			return new(dataDto, HttpStatusCode.OK);
		}
	}
}
