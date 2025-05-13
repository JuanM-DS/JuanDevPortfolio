namespace Core.Application.DTOs.Skill
{
    public record SaveSkillDTO(
        string Title,
        string Descripcion,
        Guid ProfileId
    );
}
