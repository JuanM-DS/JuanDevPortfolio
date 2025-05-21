namespace Core.Application.DTOs.Profile
{
    public record ProfileDTO(
        Guid Id,
        string Name,
        string ProfesionalTitle,
        string Description,
        string ProfileImageUrl,
        string GitHubRepositoryUrl,
        string LinkedinUrl,
        string CvUrl,
        Guid AccountId
	);
}
