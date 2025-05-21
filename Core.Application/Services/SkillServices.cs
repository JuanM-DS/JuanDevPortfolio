using Core.Application.DTOs.Skill;
using Core.Application.Interfaces.Helpers;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
using Core.Application.QueryFilters;
using Core.Application.Wrappers;
using Core.Domain.Entities;
using System.Net;

namespace Core.Application.Services
{
	public class SkillServices : BaseServices<Skill, SkillDTO, SaveSkillDTO>, ISkillServices
    {
		private readonly ISkillRepository repo;
		private readonly ITechnologyItemRepository technologyItemRepo;

		public SkillServices(ISkillRepository repo, IMapper mapper, ITechnologyItemRepository TechnologyItemRepo)
            : base(repo, mapper)
		{
			this.repo = repo;
			technologyItemRepo = TechnologyItemRepo;
		}

		public async Task<AppResponse<Empty>> AddTechnologyItemsAsync(Guid SkillId, List<Guid> itemsId)
		{

			var skillTask = _repo.GetByIdAsync(SkillId, x=>x.TechnologyItems);
			var TechnologyItems = technologyItemRepo.GetAll(new TechnologyItemFilter() { Ids = itemsId }).ToList();
			var skills = await skillTask;

			if (skills is null)
				AppError.Create("No se encontró ninguna habilidad con el Id enviado")
					.BuildResponse<Empty>(HttpStatusCode.BadRequest)
					.Throw();

			if (TechnologyItems.Any())
				AppError.Create("No se encontró ningún Ítem tecnológico con los Ids enviado")
					.BuildResponse<Empty>(HttpStatusCode.BadRequest)
					.Throw();

			skills!.TechnologyItems.ToList().AddRange(TechnologyItems);
			var result = await _repo.UpdateAsync(skills);
			if (result)
				AppError.Create("Hubo un problema al registrar los Ítem")
					.BuildResponse<Empty>(HttpStatusCode.BadRequest)
					.Throw();

			return new(HttpStatusCode.OK);
		}

		public AppResponse<List<SkillDTO>> GetAll(SkillFilter filter)
		{
			var data = repo.GetAll(filter).ToList();
			if (data is null || !data.Any())
				return new(HttpStatusCode.NoContent, "No hay elementos para mostrar");

			var dataDto = _mapper.Map<SkillDTO, Skill>(data);
			if (dataDto is null)
				AppError.Create("Hubo problemas al mappear la request")
					.BuildResponse<SkillDTO>(HttpStatusCode.InternalServerError)
					.Throw();

			return new(dataDto, HttpStatusCode.OK);
		}
	}
}
