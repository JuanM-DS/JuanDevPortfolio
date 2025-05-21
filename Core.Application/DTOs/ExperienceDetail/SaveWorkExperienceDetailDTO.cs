namespace Core.Application.DTOs.ExperienceDetail
{
    public record SaveWorkExperienceDetailDTO(
    string Title,
    string Descripcion,
    Guid ExperienceId
    );
}
