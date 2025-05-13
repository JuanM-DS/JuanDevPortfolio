using Core.Application.DTOs.ExperienceDetail;
using Core.Domain.Entities;

namespace Core.Application.Interfaces.Services
{
    public interface IExperienceDetailServices : IBaseServices<ExperienceDetail, ExperienceDetailDTO, SaveExperienceDetailDTO> { }

}
