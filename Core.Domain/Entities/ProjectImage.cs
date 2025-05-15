using Core.Domain.CommonEntities;

namespace Core.Domain.Entities
{
	public class ProjectImage : BaseEntity, IAuditableProperties
	{
		public string CreatedBy { get; set; } = string.Empty;
		public DateTime Created { get; set; }
		public string? UpdatedBy { get; set; } = string.Empty;
		public DateTime? Updated { get; set; }

		public Guid ProjectId { get; set; }
		public string ImageUrl { get; set; } = string.Empty;

		public Project Project { get; set; } = null!;
	}
}
