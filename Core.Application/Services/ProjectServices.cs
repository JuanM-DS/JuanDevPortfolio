using Core.Application.DTOs.Project;
using Core.Application.Interfaces.Helpers;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
using Core.Domain.Entities;

namespace Core.Application.Services
{
    public class ProjectServices : BaseServices<Project, ProjectDTO, SaveProjectDTO>, IProjectServices
    {
        public ProjectServices(IProjectRepository repo, IMapper mapper)
            : base(repo, mapper)
        { }
    }
}
