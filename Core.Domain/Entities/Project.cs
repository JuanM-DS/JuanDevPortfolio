namespace Core.Domain.Entities
{
	public class Project : BaseEntity, IAuditableProperties
	{
		public string CreatedBy { get; set; } = string.Empty;
		public DateTime Created { get; set; }
		public string? UpdatedBy { get; set; } = string.Empty;
		public DateTime? Updated { get; set; }

		public string Title { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public string GitHubRepositoryUrl { get; set; } = string.Empty;
		public Guid ProfileId { get; set; }

		public Profile Profile { get; set; } = null!;
		public ICollection<TechnologyItem> TechnologyItems { get; set; } = [];
		public ICollection<ProjectImage> ProjectImages { get; set; } = [];
	}
}
