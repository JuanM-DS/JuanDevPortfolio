using Core.Domain.Enumerables;
using Microsoft.AspNetCore.Http;

namespace Core.Application.DTOs.TTechnologyItem
{
    public record SaveTechnologyItemDTO(
        string Title,
		string ImageIconUrl,
		IFormFile? ImageFile,
		string Description,
        LevelsTypes LevelType
    );
}
