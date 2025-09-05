using Core.Domain.CommonEntities;
using Core.Domain.Enumerables;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Authentication.CustomEntities
{
    public class AppRole : IdentityRole<Guid>, IAuditableProperties
    {
		public AppRole(RoleType role)
		{
			Role = role;
			Name = Role.ToString();
			NormalizedName = Role.ToString().ToUpper();
		}

		public string CreatedBy { get; set; } = string.Empty;
		public DateTime Created { get; set; }
		public string? UpdatedBy { get; set; } = string.Empty;
		public DateTime? Updated { get; set; }

		public RoleType Role { get; set; }

		public ICollection<AppUser> Users { get; set; }
	}
}
