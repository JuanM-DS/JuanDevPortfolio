using Core.Domain.CommonEntities;

namespace Core.Domain.Entities
{
	public class CommentReference : BaseEntity, IAuditableProperties
	{
		public string CreatedBy { get; set; } = string.Empty;
		public DateTime Created { get; set; }
		public string? UpdatedBy { get; set; } = string.Empty;
		public DateTime? Updated { get; set; }

		public Guid AccountId { get; set; }
		public string Comment { get; set; } = string.Empty;
		public Guid ProfileId { get; set; }
		public bool IsConfirmed { get; set; }

		public Profile Profile { get; set; } = null!;
	}
}
