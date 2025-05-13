using Core.Application.DTOs.Project;
using Core.Domain.Entities;

namespace Core.Application.Interfaces.Services
{
    public interface IProjectServices : IBaseServices<Project, ProjectDTO, SaveProjectDTO> { }

}
