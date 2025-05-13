using Core.Application.DTOs.TTechnologyItem;
using Core.Domain.Entities;

namespace Core.Application.Interfaces.Services
{
    public interface ITechnologyItemServices : IBaseServices<TechnologyItem, TechnologyItemDTO, SaveTechnologyItemDTO> { }

}
