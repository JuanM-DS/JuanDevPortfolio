using Core.Domain.Enumerables;
using Infrastructure.Authentication.CustomEntities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Authentication.Seeds
{
	public static class RoleSeeds
	{
		public static async Task RegisteRolesSeedsAsync(RoleManager<AppRole> roleManager)
		{
			var adminRole = new AppRole(RoleType.Admin);
			var basicRole = new AppRole(RoleType.Basic);

			if ((await roleManager.FindByNameAsync(adminRole.Name!)) is null)
			{
				await roleManager.CreateAsync(adminRole);
			}

			if ((await roleManager.FindByNameAsync(basicRole.Name!)) is null)
			{
				await roleManager.CreateAsync(basicRole);
			}
		}
	}
}
