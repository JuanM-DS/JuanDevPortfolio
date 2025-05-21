namespace Core.Application.DTOs.Experience
{
    public record WorkExperienceDTO(
        Guid Id,
        string Position,
        string CompanyName,
        DateTime FromDate,
        DateTime? ToDate,
        string Description,
        string ImageUrl,
        Guid ProfileId
    );
}
