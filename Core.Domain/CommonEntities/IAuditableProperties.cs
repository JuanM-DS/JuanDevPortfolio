namespace Core.Domain.CommonEntities
{
	public interface IAuditableProperties
	{
		public string CreatedBy { get; set; }
		public DateTime Created { get; set; }
		public string? UpdatedBy { get; set; }
		public DateTime? Updated { get; set; }
	}
}
