using Core.Application.DTOs.ProjectImage;
using Core.Application.Interfaces.Helpers;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
using Core.Domain.Entities;

namespace Core.Application.Services
{
    public class ProjectImageServices : BaseServices<ProjectImage, ProjectImageDTO, SaveProjectImageDTO>, IProjectImageServices
    {
        public ProjectImageServices(IProjectImageRepository repo, IMapper mapper)
            : base(repo, mapper)
        { }
    }
}
