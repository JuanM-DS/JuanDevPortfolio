using Core.Domain.Enumerables;

namespace Core.Domain.Entities
{
	public class TechnologyItem : BaseEntity, IAuditableProperties
	{
		public string CreatedBy { get; set; } = string.Empty;
		public DateTime Created { get; set; }
		public string? UpdatedBy { get; set; } = string.Empty;
		public DateTime? Updated { get; set; }

		public string Title { get; set; } = string.Empty;
		public string ImageIconUrl { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public LevelsTypes LevelType { get; set; }
	}
}
