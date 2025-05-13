namespace Core.Application.DTOs.Project
{
    public record ProjectDTO(
        string Title,
        string Description,
        string GitHubRepositoryUrl,
        Guid ProfileId
    );
}
