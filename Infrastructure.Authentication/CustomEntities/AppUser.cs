using Core.Domain.CommonEntities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Authentication.CustomEntities
{
	public class AppUser : IdentityUser<Guid>, IAuditableProperties
	{
		public AppUser(string firstName, string profileImageUrl)
		{
			FirstName = firstName;
			ProfileImageUrl = profileImageUrl;
		}

		public string FirstName { get; set; } = string.Empty;
		public string ProfileImageUrl { get; set; } = string.Empty;

		public string CreatedBy { get; set; } = string.Empty;
		public DateTime Created { get; set; }
		public string? UpdatedBy { get; set; }
		public DateTime? Updated { get; set; }

		public ICollection<AppRole> Roles { get; set; }
	}
}
