using Microsoft.AspNetCore.Http;

namespace Core.Application.DTOs.Profile
{
	public record SaveProfileDTO(
        string ProfesionalTitle,
        string Description,
        string GitHubRepositoryUrl,
        string LinkedinUrl,
        IFormFile? Cv,
		Guid AccountId
	);
}
