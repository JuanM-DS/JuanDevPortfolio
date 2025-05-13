namespace Core.Application.DTOs.Project
{
    public record SaveProjectDTO(
        string Title,
        string Description,
        string GitHubRepositoryUrl
    );
}
