namespace Core.Application.DTOs.Profile
{
	public record SaveProfileDTO(
        string ProfesionalTitle,
        string Description,
        string GitHubRepositoryUrl,
        string LinkedinUrl,
        string CvUrl,
		Guid AccountId
	);
}
