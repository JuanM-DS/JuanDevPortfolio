﻿namespace Core.Domain.Entities
{
	public class ExperienceDetail : BaseEntity, IAuditableProperties
	{
		public string CreatedBy { get; set; } = string.Empty;
		public DateTime Created { get; set; }
		public string? UpdatedBy { get; set; } = string.Empty;
		public DateTime? Updated { get; set; }

		public string Title { get; set; } = string.Empty;
		public string Descripcion { get; set; } = string.Empty;
		public Guid ExperienceId { get; set; }

		public Experience Experience { get; set; } = null!;
	}
}
