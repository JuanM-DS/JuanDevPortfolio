namespace Core.Application.DTOs.Project
{
    public record ProjectDTO(
        Guid Id,
        string Title,
        string Description,
        string GitHubRepositoryUrl,
        Guid ProfileId
    );
}
