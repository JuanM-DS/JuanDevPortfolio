using Core.Domain.Enumerables;

namespace Core.Application.DTOs.TTechnologyItem
{
    public record TechnologyItemDTO(
        Guid Id,
        string Name,
        string ImageIconUrl,
        string Description,
        LevelsTypes LevelType
    );
}
