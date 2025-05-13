using Core.Application.DTOs.Skill;
using Core.Domain.Entities;

namespace Core.Application.Interfaces.Services
{
    public interface ISkillServices : IBaseServices<Skill, SkillDTO, SaveSkillDTO> { }

}
