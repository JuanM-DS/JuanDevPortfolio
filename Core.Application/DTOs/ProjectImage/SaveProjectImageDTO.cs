namespace Core.Application.DTOs.ProjectImage
{
    public record SaveProjectImageDTO(
        Guid ProjectId,
        string ImageUrl
    );
}
