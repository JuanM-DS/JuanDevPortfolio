namespace Core.Application.DTOs.ExperienceDetail
{
    public record WorkExperienceDetailDTO(
        Guid Id,
        string Title,
        string Descripcion,
        Guid ExperienceId
    );
}
