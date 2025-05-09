namespace Core.Domain.Entities
{
	public class Skills : BaseEntity, IAuditableProperties
	{
		public string CreatedBy { get; set; } = string.Empty;
		public DateTime Created { get; set; }
		public string? UpdatedBy { get; set; } = string.Empty;
		public DateTime? Updated { get; set; }

		public string Title { get; set; } = string.Empty;
		public string Descripcion { get; set; } = string.Empty;
		public Guid ProfileId { get; set; }

		public Profile Profile { get; set; } = null!;
		public ICollection<TechnologyItem> TechnologyItems { get; set; } = [];
	}
}
