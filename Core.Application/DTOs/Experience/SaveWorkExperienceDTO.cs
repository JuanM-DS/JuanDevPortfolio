using Core.Application.DTOs.ExperienceDetail;
using Core.Application.DTOs.TTechnologyItem;

namespace Core.Application.DTOs.Experience
{
    public record SaveWorkExperienceDTO
    {
		public string Position { get; set; } = string.Empty;
		public string CompanyName { get; set; } = string.Empty;
		public DateTime FromDate { get; set; }
		public DateTime? ToDate { get; set; }
		public string Description { get; set; } = string.Empty;
		public string ImageUrl { get; set; } = string.Empty;
		public Guid ProfileId { get; set; }
		public List<TechnologyItemDTO> TechnologyItems { get; set; } = [];
		public List<SaveWorkExperienceDetailDTO> ExperienceDetail { get; set; } = [];
	}
}
