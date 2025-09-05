using Core.Domain.CommonEntities;

namespace Infrastructure.Authentication.CustomEntities
{
	public class RefreshToken : IAuditableProperties
	{
		public RefreshToken(string token, Guid userId, DateTime lifeTime,string ipAddress)
		{
			Token = token;
			UserId = userId;
			LifeTime = lifeTime;
			IsActive = lifeTime > DateTime.UtcNow;
			IpAddress = ipAddress;
		}

		public string CreatedBy { get; set; } = string.Empty;
		public DateTime Created { get; set; }
		public string? UpdatedBy { get; set; } = string.Empty;
		public DateTime? Updated { get; set; }

		public Guid Id { get; set; } = Guid.NewGuid();
		public string Token { get; set; } 
		public Guid UserId { get; set; }
		public DateTime LifeTime { get; set; }
		public bool IsActive { get; set; }
		public string IpAddress { get; set; }

		public AppUser User { get; set; } = null!; 
	}
}
