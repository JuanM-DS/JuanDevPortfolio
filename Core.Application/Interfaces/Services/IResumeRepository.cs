using Microsoft.AspNetCore.Http;

namespace Core.Application.Interfaces.Services
{
	public interface IResumeRepository
	{
		public void DeleteResume(Guid ProfileId);

		public Task<string?> SaveResumeAsync(IFormFile? resume, Guid ProfileId);
	}
}
