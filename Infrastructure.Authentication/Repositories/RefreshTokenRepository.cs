using Core.Domain.Enumerables;
using Infrastructure.Authentication.Context;
using Infrastructure.Authentication.CustomEntities;
using Infrastructure.Authentication.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Infrastructure.Authentication.Repositories
{
	public class RefreshTokenRepository : IRefreshTokenRepository
	{
		private readonly IdentityContext context;

		public RefreshTokenRepository(IdentityContext context)
		{
			this.context = context;
		}
		public async Task<bool> CreateAsync(RefreshToken refreshToken)
		{
			try
			{
				await context.RefreshToken.AddAsync(refreshToken);
				var result = await context.SaveChangesAsync();
				return result > 0;
			}
			catch (Exception ex)
			{
				Log.ForContext(LoggerKeys.AuthenticationLogs.ToString(), true).Error(ex.Message);
				return false;
			}
		}

		public async Task<bool> DeleteAllByInactivityAsync()
		{
			try
			{
				await context.RefreshToken.AsQueryable().Where(x=>x.IsActive == false).ExecuteDeleteAsync();
				var result = await context.SaveChangesAsync();
				return result > 0;
			}
			catch (Exception ex)
			{
				Log.ForContext(LoggerKeys.AuthenticationLogs.ToString(), true).Error(ex.Message);
				return false;
			}
		}

		public async Task<bool> DeleteAllByIpAddressAsync(string IpAddress)
		{
			try
			{
				await context.RefreshToken.AsQueryable().Where(x => x.IpAddress == IpAddress).ExecuteDeleteAsync();
				var result = await context.SaveChangesAsync();
				return result > 0;
			}
			catch (Exception ex)
			{
				Log.ForContext(LoggerKeys.AuthenticationLogs.ToString(), true).Error(ex.Message);
				return false;
			}
		}

		public async Task<bool> DeleteAllByUserAsync(Guid UserId)
		{
			try
			{
				await context.RefreshToken.AsQueryable().Where(x => x.UserId == UserId).ExecuteDeleteAsync();
				var result = await context.SaveChangesAsync();
				return result > 0;
			}
			catch (Exception ex)
			{
				Log.ForContext(LoggerKeys.AuthenticationLogs.ToString(), true).Error(ex.Message);
				return false;
			}
		}

		public async Task<bool> DeleteAsync(RefreshToken refreshToken)
		{
			try
			{
				context.RefreshToken.Remove(refreshToken);
				var result = await context.SaveChangesAsync();
				return result > 0;
			}
			catch (Exception ex)
			{
				Log.ForContext(LoggerKeys.AuthenticationLogs.ToString(), true).Error(ex.Message);
				return false;
			}
		}

		public IEnumerable<RefreshToken> GetAll()
		{
			return context.RefreshToken;
		}

		public IEnumerable<RefreshToken> GetAllByUserIdAsync(Guid UserId)
		{
			return context.RefreshToken.Where(x => x.UserId == UserId);
		}

		public async Task<RefreshToken?> GetByIdAsync(Guid Id)
		{
			return await context.RefreshToken.FirstOrDefaultAsync(x => x.Id == Id);
		}

		public async Task<RefreshToken?> GetByTokenAsync(string Token)
		{
			return await context.RefreshToken.FirstOrDefaultAsync(x => x.Token== Token);
		}

		public async Task<RefreshToken?> GetByIpAddressAsync(string IpAddress)
		{
			return await context.RefreshToken.FirstOrDefaultAsync(x => x.IpAddress == IpAddress);
		}

		public async Task<bool> UpdateAsync(RefreshToken refreshToken)
		{
			try
			{
				context.RefreshToken.Update(refreshToken);
				var result = await context.SaveChangesAsync();
				return result > 0;
			}
			catch (Exception ex)
			{
				Log.ForContext(LoggerKeys.AuthenticationLogs.ToString(), true).Error(ex.Message);
				return false;
			}
		}
	}
}
