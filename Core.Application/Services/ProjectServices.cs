using Core.Application.DTOs.Project;
using Core.Application.Interfaces.Helpers;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
using Core.Application.QueryFilters;
using Core.Application.Wrappers;
using Core.Domain.Entities;
using System.Net;

namespace Core.Application.Services
{
    public class ProjectServices : BaseServices<Project, ProjectDTO, SaveProjectDTO>, IProjectServices
    {
		private readonly IProjectRepository repo;
		private readonly ITechnologyItemRepository technologyItemRepo;

		public ProjectServices(IProjectRepository repo, IMapper mapper, ITechnologyItemRepository TechnologyItemRepo)
            : base(repo, mapper)
		{
			this.repo = repo;
			technologyItemRepo = TechnologyItemRepo;
		}

		public async Task<AppResponse<Empty>> AddTechnologyItemsAsync(Guid ProjectId, List<Guid> itemsId)
		{
			var projectTask = _repo.GetByIdAsync(ProjectId, x => x.TechnologyItems);
			var TechnologyItems = technologyItemRepo.GetAll(new TechnologyItemFilter() { Ids = itemsId }).ToList();
			var project = await projectTask;

			if (project is null)
				AppError.Create("No se encontró ninguna habilidad con el Id enviado")
					.BuildResponse<Empty>(HttpStatusCode.BadRequest)
					.Throw();

			if (TechnologyItems.Any())
				AppError.Create("No se encontró ningún Ítem tecnológico con los Ids enviado")
					.BuildResponse<Empty>(HttpStatusCode.BadRequest)
					.Throw();

			project!.TechnologyItems.ToList().AddRange(TechnologyItems);
			var result = await _repo.UpdateAsync(project);
			if (result)
				AppError.Create("Hubo un problema al registrar los Ítem")
					.BuildResponse<Empty>(HttpStatusCode.BadRequest)
					.Throw();

			return new(HttpStatusCode.OK);
		}

		public AppResponse<List<ProjectDTO>> GetAll(ProjectFilter filter)
		{
			var data = repo.GetAll(filter).ToList();
			if (data is null || !data.Any())
				return new(HttpStatusCode.NoContent, "No hay elementos para mostrar");

			var dataDto = _mapper.Map<ProjectDTO,Project> (data);
			if (dataDto is null)
				AppError.Create("Hubo problemas al mappear la request")
					.BuildResponse<ProjectDTO>(HttpStatusCode.InternalServerError)
					.Throw();

			return new(dataDto, HttpStatusCode.OK);
		}

	}
}
