using Core.Application.DTOs.TTechnologyItem;
using Core.Application.Interfaces.Helpers;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
using Core.Domain.Entities;

namespace Core.Application.Services
{
    public class TechnologyItemServices : BaseServices<TechnologyItem, TechnologyItemDTO, SaveTechnologyItemDTO>, ITechnologyItemServices
    {
        public TechnologyItemServices(ITechnologyItemRepository repo, IMapper mapper)
            : base(repo, mapper)
        { }
    }
}
