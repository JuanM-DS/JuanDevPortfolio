using Core.Application.DTOs.Experience;
using Core.Application.Interfaces.Helpers;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
using Core.Domain.Entities;

namespace Core.Application.Services
{
    public class ExperienceServices : BaseServices<Experience, ExperienceDTO, SaveExperienceDTO>, IExperienceServices
    {
        public ExperienceServices(IExperienceRepository repo, IMapper mapper)
            : base(repo, mapper)
        { }
    }
}
