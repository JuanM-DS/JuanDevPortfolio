using Core.Application.DTOs.Profile;
using Core.Domain.Entities;

namespace Core.Application.Helpers
{
    public static class MappingProfiles
    {
        public static Profile Map(ProfileDTO source)
        {
            return new Profile() 
            {
                CvUrl = source.CvUrl,
                Description = source.Description,
                GitHubRepositoryUrl = source.GitHubRepositoryUrl,
                Id = source.Id,
                LinkedinUrl = source.LinkedinUrl,
                Name = source.Name,
                ProfesionalTitle = source.ProfesionalTitle,
                ProfileImageUrl = source.ProfileImageUrl
            };
        }

        public static ProfileDTO Map(Profile source)
        {
            return new ProfileDTO(
                source.Id,
                source.Name,
                source.ProfesionalTitle,
                source.Description,
                source.ProfileImageUrl,
                source.GitHubRepositoryUrl,
                source.LinkedinUrl,
                source.CvUrl);
        }
    }
}
