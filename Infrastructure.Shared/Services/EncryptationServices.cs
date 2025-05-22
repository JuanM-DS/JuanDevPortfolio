using Core.Application.Interfaces.Shared;
using Microsoft.AspNetCore.DataProtection;

namespace Infrastructure.Shared.Services
{
	public class EncryptationServices : IEncryptationServices
	{
		private readonly IDataProtector DataProtector;
		public EncryptationServices(IDataProtectionProvider DataProtectionProvider)
		{
			DataProtector = DataProtectionProvider.CreateProtector("JuanDevPortfolioProposit");
		}
		public string Encrypt(string srt)
		{
			return DataProtector.Protect(srt);
		}

		public string Decrypt(string srt)
		{
			return DataProtector.Unprotect(srt);
		}
	}
}
