using Infrastructure.Authentication.CustomEntities;

namespace Infrastructure.Authentication.Interfaces
{
	public interface IRefreshTokenRepository
	{
		public Task<bool> CreateAsync(RefreshToken refreshToken);
		public Task<bool> UpdateAsync(RefreshToken refreshToken);
		public Task<bool> DeleteAllByIpAddressAsync(string IpAddress);
		public Task<bool> DeleteAllByUserAsync(Guid UserId);
		public Task<bool> DeleteAllByInactivityAsync();
		public Task<bool> DeleteAsync(RefreshToken refreshToken);

		public Task<RefreshToken?> GetByIdAsync(Guid Id);
		public Task<RefreshToken?> GetByTokenAsync(string Token);
		public Task<RefreshToken?> GetByIpAddressAsync(string IpAddress);
		public IEnumerable<RefreshToken> GetAllByUserIdAsync(Guid UserId);
		public IEnumerable<RefreshToken> GetAll();
	}
}
