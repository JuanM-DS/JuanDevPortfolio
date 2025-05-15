using Core.Application.DTOs.ExperienceDetail;
using Core.Application.Interfaces.Helpers;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
using Core.Domain.Entities;

namespace Core.Application.Services
{
    public class ExperienceDetailServices : BaseServices<ExperienceDetail, ExperienceDetailDTO, SaveExperienceDetailDTO>, IExperienceDetailServices
    {
        public ExperienceDetailServices(IExperienceDetailRepository repo, IMapper mapper)
            : base(repo, mapper)
        { }
    }
}
