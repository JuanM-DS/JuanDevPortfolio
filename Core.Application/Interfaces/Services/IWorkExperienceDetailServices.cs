using Core.Application.DTOs.ExperienceDetail;
using Core.Application.QueryFilters;
using Core.Application.Wrappers;
using Core.Domain.Entities;

namespace Core.Application.Interfaces.Services
{
	public interface IWorkExperienceDetailServices : IBaseServices<WorkExperienceDetail, WorkExperienceDetailDTO, SaveWorkExperienceDetailDTO>
    {
		public AppResponse<List<WorkExperienceDetailDTO>> GetAll(WorkExperienceDetailFilter filter);
	}
}
