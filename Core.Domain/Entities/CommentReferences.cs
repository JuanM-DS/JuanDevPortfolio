namespace Core.Domain.Entities
{
	public class CommentReferences : BaseEntity, IAuditableProperties
	{
		public string CreatedBy { get; set; } = string.Empty;
		public DateTime Created { get; set; }
		public string? UpdatedBy { get; set; } = string.Empty;
		public DateTime? Updated { get; set; }

		public string PersonName { get; set; } = string.Empty;
		public string? ProfileImageUrl { get; set; }
		public string Comment { get; set; } = string.Empty;
		public Guid ProfileId { get; set; }

		public Profile Profile { get; set; } = null!;
	}
}
