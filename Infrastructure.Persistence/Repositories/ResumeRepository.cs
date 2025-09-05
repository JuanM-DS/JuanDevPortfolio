using Core.Application.Interfaces.Services;
using Core.Domain.Enumerables;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace Infrastructure.Persistence.Repositories
{
	public class ResumeRepository : IResumeRepository
	{
		public void DeleteResume(Guid ProfileId)
		{
			var baseRoot = Path.Combine(Directory.GetCurrentDirectory(),"Media", "Resumes");
			var finalPath = Path.Combine(baseRoot, ProfileId.ToString());

			if (File.Exists(finalPath))
			{
				File.Delete(finalPath);
				return;
			}
			Log.ForContext(LoggerKeys.SharedLogs.ToString(), true).Error("La carpeta Default no existe: {Folder}", finalPath);
		}

		public async Task<string?> SaveResumeAsync(IFormFile? resume, Guid ProfileId)
		{
			if (resume is null || resume.Length == 0)
			{
				Log.ForContext(LoggerKeys.SharedLogs.ToString(), true).Error("Ningun archivo fue enviado como curriculum");
				return null;
			}

			var baseRoot = Path.Combine(Directory.GetCurrentDirectory(), "Media", "Resumes");
			var finalPath = Path.Combine(baseRoot, ProfileId.ToString());

			if (!Directory.Exists(baseRoot))
			{
				Directory.CreateDirectory(baseRoot);
			}
			var allowedExtensions = new[] { ".pdf" };
			var extension = Path.GetExtension(resume.FileName).ToLowerInvariant();
			var contentType = resume.ContentType.ToLowerInvariant();
			if (!allowedExtensions.Contains(extension) || contentType != "application/pdf")
			{
				Log.ForContext(LoggerKeys.SharedLogs.ToString(), true).Error("El cv enviado debe ser un Pdf");
				return null;
			}
			finalPath = string.Concat(finalPath, extension);
			using var stream = new FileStream(finalPath, FileMode.Create, FileAccess.Write, FileShare.None);
			await resume.CopyToAsync(stream);

			return string.Concat("Resumes", "/", ProfileId, extension);
		}
	}
}
