﻿using Core.Domain.CommonEntities;

namespace Core.Domain.Entities
{
	public class WorkExperience : BaseEntity, IAuditableProperties
	{
		public string CreatedBy { get; set; } = string.Empty;
		public DateTime Created { get; set; }
		public string? UpdatedBy { get; set; } = string.Empty;
		public DateTime? Updated { get; set; }

		public string Position { get; set; } = string.Empty;
		public string CompanyName { get; set; } = string.Empty;
		public DateTime FromDate { get; set; }
		public DateTime? ToDate { get; set; }
		public string Description { get; set; } = string.Empty;
		public string CompanyLogoUrl { get; set; } = string.Empty;
		public Guid ProfileId { get; set; }

		public Profile Profile { get; set; } = null!;
		public ICollection<TechnologyItem> TechnologyItems { get; set; } = [];
		public ICollection<WorkExperienceDetail> ExperienceDetails { get; set; } = [];
	}
}
