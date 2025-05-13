using Core.Application.DTOs.Experience;
using Core.Domain.Entities;

namespace Core.Application.Interfaces.Services
{
    public interface IExperienceServices : IBaseServices<Experience, ExperienceDTO, SaveExperienceDTO> { }

}
