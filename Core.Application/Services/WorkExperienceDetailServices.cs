using Core.Application.DTOs.ExperienceDetail;
using Core.Application.Interfaces.Helpers;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
using Core.Application.QueryFilters;
using Core.Application.Wrappers;
using Core.Domain.Entities;
using System.Net;

namespace Core.Application.Services
{
    public class WorkExperienceDetailServices : BaseServices<WorkExperienceDetail, WorkExperienceDetailDTO, SaveWorkExperienceDetailDTO>, IWorkExperienceDetailServices
    {
		private readonly IWorkExperienceDetailRepository repo;

		public WorkExperienceDetailServices(IWorkExperienceDetailRepository repo, IMapper mapper)
            : base(repo, mapper)
		{
			this.repo = repo;
		}

		public AppResponse<List<WorkExperienceDetailDTO>> GetAll(WorkExperienceDetailFilter filter)
		{
			var data = repo.GetAll(filter).ToList();
			if (data is null || !data.Any())
				return new(HttpStatusCode.NoContent, "No hay elementos para mostrar");

			var dataDto = _mapper.Map<WorkExperienceDetailDTO, WorkExperienceDetail>(data);
			if (dataDto is null)
				AppError.Create("Hubo problemas al mappear la request")
					.BuildResponse<WorkExperienceDetailDTO>(HttpStatusCode.InternalServerError)
					.Throw();

			return new(dataDto, HttpStatusCode.OK);
		}
	}
}
