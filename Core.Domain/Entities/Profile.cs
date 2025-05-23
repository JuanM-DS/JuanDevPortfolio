using Core.Domain.CommonEntities;

namespace Core.Domain.Entities
{
	public class Profile : BaseEntity, IAuditableProperties
	{
		public string CreatedBy { get; set; } = string.Empty;
		public DateTime Created { get; set; }
		public string? UpdatedBy { get; set; } = string.Empty;
		public DateTime? Updated { get; set; }

		public string ProfesionalTitle { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;

		public string GitHubRepositoryUrl { get; set; } = string.Empty;
		public string LinkedinUrl { get; set; } = string.Empty;
		public string CvUrl { get; set; } = string.Empty;
		public Guid AccountId { get; set; }

		public ICollection<CommentReference> ComenntReferences { get; set; } = [];
		public ICollection<Skill> Skills { get; set; } = [];
		public ICollection<WorkExperience> Experiences { get; set; } = [];
		public ICollection<Project> Projects { get; set; } = [];
	}
}
