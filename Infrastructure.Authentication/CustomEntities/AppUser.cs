using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Authentication.CustomEntities
{
	public class AppUser : IdentityUser<Guid>
	{
		public AppUser(string firstName, string profileImageUrl)
		{
			FirstName = firstName;
			ProfileImageUrl = profileImageUrl;
		}

		public string FirstName { get; set; } = string.Empty;
		public string ProfileImageUrl { get; set; } = string.Empty;
	}
}
