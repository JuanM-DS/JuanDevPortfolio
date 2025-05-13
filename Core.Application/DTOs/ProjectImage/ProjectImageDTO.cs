namespace Core.Application.DTOs.ProjectImage
{
    public record ProjectImageDTO(
        Guid Id,
        Guid ProjectId,
        string ImageUrl
    );
}
