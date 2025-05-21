using Core.Application.DTOs.ProjectImage;
using Core.Application.QueryFilters;
using Core.Application.Wrappers;
using Core.Domain.Entities;

namespace Core.Application.Interfaces.Services
{
	public interface IProjectImageServices : IBaseServices<ProjectImage, ProjectImageDTO, SaveProjectImageDTO>
    {
		public AppResponse<List<ProjectImageDTO>> GetAll(ProjectImageFilter filter);
	}
}
