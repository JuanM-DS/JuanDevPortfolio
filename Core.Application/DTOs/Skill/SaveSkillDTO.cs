using Core.Application.DTOs.TTechnologyItem;

namespace Core.Application.DTOs.Skill
{
    public class SaveSkillDTO
    {
		public string Title { get; set; } = string.Empty;
		public string Descripcion { get; set; } = string.Empty;
		public Guid ProfileId { get; set; }
		public List<SaveTechnologyItemDTO> TechnologyItems { get; set; } = [];
	}
}
