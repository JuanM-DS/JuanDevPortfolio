using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using Infrastructure.Persistence.Context;

namespace Infrastructure.Persistence.Repositories
{
    public class CommentReferencesRepository : BaseRepository<CommentReferences>, ICommentReferencesRepository
    {
        public CommentReferencesRepository(MainContext context)
            :base(context)
        {}
    }
}
