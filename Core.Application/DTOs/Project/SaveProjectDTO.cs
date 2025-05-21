using Core.Application.DTOs.ProjectImage;
using Core.Application.DTOs.TTechnologyItem;

namespace Core.Application.DTOs.Project
{
    public record SaveProjectDTO
    {
		public string Title { get; set; } = string.Empty;

		public string Description { get; set; } = string.Empty;

		public string GitHubRepositoryUrl { get; set; } = string.Empty;

		public List<SaveTechnologyItemDTO> TechnologyItems { get; set; } = [];
		public List<SaveProjectImageDTO> ProjectImages { get; set; } = [];

	}
}
