using Microsoft.AspNetCore.Http;

namespace Core.Application.Interfaces.Shared
{
	public interface IImageRepository
	{
		public Task<string?> SaveImageAsync(IFormFile file, string directoryEntity, string id, string? oldImagePath = null);

		public string GetDefaultImageUrl(string directoryEntity);

		public bool DeleteImage(string imagePath);
	}
}
