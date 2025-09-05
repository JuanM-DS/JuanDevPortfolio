using Infrastructure.Authentication.CustomEntities;

namespace Infrastructure.Authentication.Interfaces
{
	public interface ITokenServices
	{
		public Task<bool> DeleteInactiveRefreshTokensAsync();
		public Task<bool> DeleteRefreshTokenAsync(Guid tokenId);
		public Task<bool> DeleteRefreshTokenAsync(string refreshToken);
		public Task<bool> DeleteRefreshTokensByIpAsync(string Ip);
		public Task<bool> DeleteRefreshTokensByUserAsync(Guid userId);
		public Task<string> GenerateAccessJwtToken(AppUser user);
		public Task<string> GenerateRefreshToken(Guid userId);
		public Task<string?> UpdateRefreshTokenAsync(string token);

		public bool IsAccessTokenValid(string token);
		public Task<bool> IsRefreshTokenValid(string token);
	}
}