namespace Core.Application.DTOs.ExperienceDetail
{
    public record ExperienceDetailDTO(
        Guid Id,
        string Title,
        string Descripcion,
        Guid ExperienceId
    );
}
