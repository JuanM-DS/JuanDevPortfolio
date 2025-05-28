using Core.Application.Interfaces.Shared;
using Core.Domain.Enumerables;
using Infrastructure.Authentication.CustomEntities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Authentication.Seeds
{
	public static class UserSeeds
	{
		public static async Task RegisteAdminUserSeed(UserManager<AppUser> userManager, IImageRepository imageRepository)
		{
			var defualImage = imageRepository.GetDefaultImageUrl("User");
			var adminUser = new AppUser(RoleType.Admin.ToString(), defualImage) { Email = "admin@gmail.com", UserName = RoleType.Admin.ToString() };

			if ((await userManager.FindByNameAsync(adminUser!.UserName) is null))
				await userManager.CreateAsync(adminUser, "Pa$$word!123");


			if ((await userManager.FindByEmailAsync(adminUser!.Email) is null))
				await userManager.AddToRoleAsync(adminUser, RoleType.Admin.ToString());
		}
	}
}
