using Core.Domain.Enumerables;

namespace Core.Application.DTOs.TTechnologyItem
{
    public record SaveTechnologyItemDTO(
        string Title,
        string ImageIconUrl,
        string Description,
        LevelsTypes LevelType
    );
}
