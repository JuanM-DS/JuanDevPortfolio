using Microsoft.AspNetCore.Http;

namespace Core.Application.DTOs.ProjectImage
{
    public record SaveProjectImageDTO(
        Guid ProjectId,
		IFormFile? ImageFile,
		string ImageUrl
	);
}
