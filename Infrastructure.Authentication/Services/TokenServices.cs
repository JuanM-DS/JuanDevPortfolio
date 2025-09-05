using Core.Application.Interfaces.Shared;
using Core.Domain.Enumerables;
using Core.Domain.Settings;
using Infrastructure.Authentication.CustomEntities;
using Infrastructure.Authentication.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Authentication.Services
{
	public class TokenServices : ITokenServices
	{
		private readonly UserManager<AppUser> userManager;
		private readonly IRefreshTokenRepository refreshTokenRepository;
		private readonly IHttpContextProvider httpContextProvider;
		private JwtSettings jwtSettings;

		public TokenServices(UserManager<AppUser> UserManager, IOptions<JwtSettings> JwtSettings, IRefreshTokenRepository RefreshTokenRepository, IHttpContextProvider HttpContextProvider)
		{
			userManager = UserManager;
			refreshTokenRepository = RefreshTokenRepository;
			httpContextProvider = HttpContextProvider;
			jwtSettings = JwtSettings.Value;
		}

		public async Task<string> GenerateRefreshToken(Guid userId)
		{
			var ip = httpContextProvider.GetUserIpAddress();
			var tokenByIp = await refreshTokenRepository.GetByIpAddressAsync(ip);
			if (tokenByIp is not null && tokenByIp.IsActive)
				return tokenByIp.Token;

			var token = GenerateRandomToken();
			var refreshToken = new RefreshToken(token, userId, DateTime.UtcNow.AddDays(7), ip);

			await refreshTokenRepository.CreateAsync(refreshToken);
			return token;
		}

		public async Task<string> GenerateAccessJwtToken(AppUser user)
		{
			var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey));
			var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
			var header = new JwtHeader(credentials);

			var userClaims = await userManager.GetClaimsAsync(user);
			var roleClaims = (await userManager.GetRolesAsync(user)).Select(x => new Claim(ClaimTypes.Role, x));
			var claims = new List<Claim>()
			{
				new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(JwtRegisteredClaimNames.Email, user.Email!),
				new Claim("UserId", user.Id.ToString())
			}
			.Union(userClaims).Union(roleClaims);

			var payload = new JwtPayload
				(
				jwtSettings.Issuer,
				jwtSettings.Audience,
				claims,
				DateTime.UtcNow,
				DateTime.UtcNow.AddMinutes(jwtSettings.DurationInMinutes)
				);

			var token = new JwtSecurityToken(header, payload);
			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		public async Task<string?> UpdateRefreshTokenAsync(string token)
		{
			var existingToken = await refreshTokenRepository.GetByTokenAsync(token);
			if (existingToken is null)
				return null;

			existingToken.Token = GenerateRandomToken();
			existingToken.LifeTime = DateTime.UtcNow.AddDays(7); 
			await refreshTokenRepository.UpdateAsync(existingToken);

			return existingToken.Token;
		}

		public async Task<bool> DeleteRefreshTokensByIpAsync(string ip)
		{
			var currentIp = httpContextProvider.GetUserIpAddress();
			if (currentIp != ip)
				return false;
			return await refreshTokenRepository.DeleteAllByIpAddressAsync(ip);
		}

		public async Task<bool> DeleteRefreshTokensByUserAsync(Guid userId)
		{
			var currentId = httpContextProvider.GetCurrentUserId();
			if (currentId != userId)
				return false;
			return await refreshTokenRepository.DeleteAllByUserAsync(userId);
		}

		public async Task<bool> DeleteInactiveRefreshTokensAsync()
		{
			return await refreshTokenRepository.DeleteAllByInactivityAsync();
		}

		public async Task<bool> DeleteRefreshTokenAsync(Guid tokenId)
		{
			var token = await refreshTokenRepository.GetByIdAsync(tokenId);
			var currentId = httpContextProvider.GetCurrentUserId();
			if (token!.UserId != currentId)
				return false;

			if (token is null)
				return false;

			return await refreshTokenRepository.DeleteAsync(token);
		}

		public bool IsAccessTokenValid(string token)
		{
			var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);

			if (jwt.ValidTo < DateTime.UtcNow)
				return false;

			return true;
		}

		public async Task<bool> IsRefreshTokenValid(string token)
		{
			var refreshToken = await refreshTokenRepository.GetByTokenAsync(token);
			if (refreshToken is null)
				return false;

			if (refreshToken.UserId != httpContextProvider.GetCurrentUserId())
			{
				Log.ForContext(LoggerKeys.AuthenticationLogs.ToString(), true).Error("El usuario en el refresh refreshToken no corresponde con el usuario en sesion");
				return false;
			}

			if (!refreshToken.IsActive)
			{
				Log.ForContext(LoggerKeys.AuthenticationLogs.ToString(), true).Error("El refresh refreshToken no esta activo");
				return false;
			}

			if (refreshToken.IpAddress != httpContextProvider.GetUserIpAddress())
			{
				Log.ForContext(LoggerKeys.AuthenticationLogs.ToString(), true).Error("La Ip en el refresh refreshToken no corresponde con la enviada");
				return false;
			}

			return true;
		}
		
		public async Task<bool> DeleteRefreshTokenAsync(string refreshToken)
		{
			var token = await refreshTokenRepository.GetByTokenAsync(refreshToken);
			var currentId = httpContextProvider.GetCurrentUserId();
			if (token is null || currentId is null)
				return false;

			if (token!.UserId != currentId)
				return false;
			if (token is null)
				return false;

			return await refreshTokenRepository.DeleteAsync(token);
		}
		
		private string GenerateRandomToken()
		{
			return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
		}
	}
}
