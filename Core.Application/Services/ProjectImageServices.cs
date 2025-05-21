using Core.Application.DTOs.ProjectImage;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
using Core.Application.Mappings;
using Core.Application.QueryFilters;
using Core.Application.Wrappers;
using Core.Domain.Entities;
using System.Net;

namespace Core.Application.Services
{
	public class ProjectImageServices : BaseServices<ProjectImage, ProjectImageDTO, SaveProjectImageDTO>, IProjectImageServices
    {
		private readonly IProjectImageRepository repo;

		public ProjectImageServices(IProjectImageRepository repo)
            : base(repo)
		{
			this.repo = repo;
		}

		public AppResponse<List<ProjectImageDTO>> GetAll(ProjectImageFilter filter)
		{
			var data = repo.GetAll(filter).ToList();
			if (data is null || !data.Any())
				return new(HttpStatusCode.NoContent, "No hay elementos para mostrar");

			var dataDto = Mapper.Map<ProjectImageDTO, ProjectImage>(data);
			if (dataDto is null)
				AppError.Create("Hubo problemas al mappear la request")
					.BuildResponse<ProjectImageDTO>(HttpStatusCode.InternalServerError)
					.Throw();

			return new(dataDto, HttpStatusCode.OK);
		}
	}
}
