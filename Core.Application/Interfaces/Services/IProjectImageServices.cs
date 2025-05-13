using Core.Application.DTOs.ProjectImage;
using Core.Domain.Entities;

namespace Core.Application.Interfaces.Services
{
    public interface IProjectImageServices : IBaseServices<ProjectImage, ProjectImageDTO, SaveProjectImageDTO> { }

}
