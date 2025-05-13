namespace Core.Application.DTOs.Profile
{
    public record ProfileDTO(
        string Name,
        string ProfesionalTitle,
        string Description,
        string ProfileImageUrl,
        string GitHubRepositoryUrl,
        string LinkedinUrl,
        string CvUrl
    );
}
