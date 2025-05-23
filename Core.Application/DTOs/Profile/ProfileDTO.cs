namespace Core.Application.DTOs.Profile
{
    public record ProfileDTO(
        Guid Id,
        string ProfesionalTitle,
        string Description,
        string GitHubRepositoryUrl,
        string LinkedinUrl,
        string CvUrl,
        Guid AccountId
	);
}
