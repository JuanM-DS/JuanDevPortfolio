using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using Infrastructure.Persistence.Context;

namespace Infrastructure.Persistence.Repositories
{
    public class TechnologyItemRepository : BaseRepository<TechnologyItem>, ITechnologyItemRepository
    {
        public TechnologyItemRepository(MainContext context)
            : base(context)
        { }
    }


}
