using Core.Application.Interfaces.Repositories;
using Core.Application.QueryFilters;
using Core.Domain.Entities;
using Infrastructure.Persistence.Context;

namespace Infrastructure.Persistence.Repositories
{
    public class CommentReferencesRepository : BaseRepository<CommentReference>, ICommentReferencesRepository
    {
        public CommentReferencesRepository(MainContext context)
            :base(context)
        {}

		public override Task<bool> UpdateAsync(CommentReference entity)
		{
			entity.IsConfirmed = false;
			return base.UpdateAsync(entity);
		}

		public IEnumerable<CommentReference> GetAll(CommentReferenceFilter filter)
		{
			var query = _entity.AsQueryable();

			if (filter.ProfileId is not null)
				query = query.Where(x => x.ProfileId == filter.ProfileId);

			if (filter.IsConfirmed is not null)
				query = query.Where(x => filter.IsConfirmed == x.IsConfirmed);

			return query.AsEnumerable();
		}
	}
}
