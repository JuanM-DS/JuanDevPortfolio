namespace Core.Application.DTOs.Experience
{
    public record SaveExperienceDTO(
        string Position,
        string CompanyName,
        DateTime FromDate,
        DateTime? ToDate,
        string Description,
        string ImageUrl,
        Guid ProfileId
    );
}
