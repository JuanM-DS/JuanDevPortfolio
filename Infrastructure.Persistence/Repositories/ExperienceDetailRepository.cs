using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using Infrastructure.Persistence.Context;

namespace Infrastructure.Persistence.Repositories
{
    public class ExperienceDetailRepository : BaseRepository<ExperienceDetail>, IExperienceDetailRepository
    {
        public ExperienceDetailRepository(MainContext context)
            : base(context)
        { }
    }


}
