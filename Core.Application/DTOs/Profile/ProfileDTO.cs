using Core.Application.DTOs.Authentication;

namespace Core.Application.DTOs.Profile
{
    public class ProfileDTO
    {
        public Guid Id { get; set; }
        public string ProfesionalTitle { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public string GitHubRepositoryUrl { get; set; } = string.Empty;
		public string LinkedinUrl { get; set; } = string.Empty;
		public string CvUrl { get; set; } = string.Empty;
		public Guid AccountId { get; set; }
		public UserDTO User { get; set; } = null!;

    }
}
