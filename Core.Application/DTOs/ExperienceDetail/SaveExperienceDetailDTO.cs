namespace Core.Application.DTOs.ExperienceDetail
{
    public record SaveExperienceDetailDTO(
    string Title,
    string Descripcion,
    Guid ExperienceId
    );
}
