namespace Core.Application.DTOs.Skill
{
    public record SkillDTO(
        string Title,
        string Descripcion,
        Guid ProfileId
    );
}
