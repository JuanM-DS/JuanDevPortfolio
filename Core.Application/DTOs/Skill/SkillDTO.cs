namespace Core.Application.DTOs.Skill
{
    public record SkillDTO(
        Guid Id,
        string Title,
        string Descripcion,
        Guid ProfileId
    );
}
