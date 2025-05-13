namespace Core.Application.DTOs.Experience
{
    public record ExperienceDTO(
        string Position,
        string CompanyName,
        DateTime FromDate,
        DateTime? ToDate,
        string Description,
        string ImageUrl,
        Guid ProfileId
    );
}
