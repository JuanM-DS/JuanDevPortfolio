using Core.Application.DTOs.Skill;
using Core.Application.Interfaces.Helpers;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
using Core.Domain.Entities;

namespace Core.Application.Services
{
    public class SkillServices : BaseServices<Skill, SkillDTO, SaveSkillDTO>, ISkillServices
    {
        public SkillServices(ISkillRepository repo, IMapper mapper)
            : base(repo, mapper)
        { }
    }
}
