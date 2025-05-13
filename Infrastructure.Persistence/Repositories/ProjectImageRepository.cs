using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using Infrastructure.Persistence.Context;

namespace Infrastructure.Persistence.Repositories
{
    public class ProjectImageRepository : BaseRepository<ProjectImage>, IProjectImageRepository
    {
        public ProjectImageRepository(MainContext context)
            : base(context)
        { }
    }


}
