using Core.Domain.Enumerables;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Authentication.CustomEntities
{
    public class AppRole : IdentityRole<Guid>
    {
		public AppRole(RoleType role)
		{
			Role = role;
			Name = Role.ToString();
			NormalizedName = Role.ToString().ToUpper();
		}

		public RoleType Role { get; set; }
    }
}
