namespace Core.Domain.Entities
{
	public class Profile : BaseEntity, IAuditableProperties
	{
		public string CreatedBy { get; set; } = string.Empty;
		public DateTime Created { get; set; }
		public string? UpdatedBy { get; set; } = string.Empty;
		public DateTime? Updated { get; set; }

		public string Name { get; set; } = string.Empty;
		public string TProfesionalTitle { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public string ProfileImageUrl { get; set; } = string.Empty;

		public string GitHubRepositoryUrl { get; set; } = string.Empty;
		public string LinkedinUrl { get; set; } = string.Empty;
		public string CvUrl { get; set; } = string.Empty;

		public ICollection<ComenntReferences> ComenntReferences { get; set; } = [];
		public ICollection<Skills> Skills { get; set; } = [];
		public ICollection<Experience> Experiences { get; set; } = [];
		public ICollection<Project> Projects { get; set; } = [];
	}
}
