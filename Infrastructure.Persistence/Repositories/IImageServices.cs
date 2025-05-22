using Core.Application.Interfaces.Shared;
using Core.Domain.Enumerables;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace Infrastructure.Persistence.Repositories
{
	public class ImageRepository : IImageRepository
	{
		public string? GetDefaultImageUrl(string directoryEntity)
		{
			if (string.IsNullOrWhiteSpace(directoryEntity))
				return null;

			var folderPath = Path.Combine(
				Directory.GetCurrentDirectory(),      
				"Media",
				"Images",
				directoryEntity,
				"default"
			);

			if (!Directory.Exists(folderPath))
			{
				Log.ForContext(LoggerKeys.SharedLogs.ToString(), true).Error("La carpeta Default no existe: {Folder}", folderPath);
				return null;
			}

			var allowedExt = new[] { ".jpg", ".jpeg", ".png", ".gif" };
			var file = Directory
				.EnumerateFiles(folderPath)
				.FirstOrDefault(f => allowedExt.Contains(Path.GetExtension(f).ToLowerInvariant()));

			if (file == null)
			{
				Log.ForContext(LoggerKeys.SharedLogs.ToString(), true).Error("No se encontró ninguna imagen en: {Folder}", folderPath);
				return null;
			}

			var fileName = Path.GetFileName(file);
			var url = Path.Combine(
				"/",
				"media",
				"images",
				directoryEntity,
				"default",
				fileName
			).Replace("\\", "/");

			return url;
		}

		public async Task<string?> SaveImageAsync(IFormFile file, string directoryEntity, string id, string? oldImagePath = null)
		{
			if (file is null || file.Length == 0)
			{
				Log.ForContext(LoggerKeys.SharedLogs.ToString(), true).Error("No se encontró ninguna IFormFile");
				return null;
			}

			if (!string.IsNullOrEmpty(oldImagePath) && File.Exists(oldImagePath))
			{
				File.Delete(oldImagePath);
			}

			var root = Directory.GetCurrentDirectory();
			var folderPath = Path.Combine(root, "Media", "Images", directoryEntity, id);

			if (!Directory.Exists(folderPath))
				Directory.CreateDirectory(folderPath);

			var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
			var allowed = new[] { ".jpg", ".jpeg", ".png", ".gif" };
			if (!allowed.Contains(extension))
			{
				Log.ForContext(LoggerKeys.SharedLogs.ToString(), true).Error("La extension de la imagen no es correcta");
				return null;
			}

			var fileName = $"{Guid.NewGuid()}{extension}";
			var fullPath = Path.Combine(folderPath, fileName);

			await using var stream = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None);
			await file.CopyToAsync(stream);

			return fullPath;
		}
	}
}
