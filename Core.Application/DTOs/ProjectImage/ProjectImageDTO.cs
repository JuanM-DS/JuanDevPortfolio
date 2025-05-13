namespace Core.Application.DTOs.ProjectImage
{
    public record ProjectImageDTO(
        Guid ProjectId,
        string ImageUrl
    );
}
