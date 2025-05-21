using Core.Application.QueryFilters;
using Core.Domain.Entities;

namespace Core.Application.Interfaces.Repositories
{
    public interface ICommentReferencesRepository : IBaseRepository<CommentReference>
    {
		public IEnumerable<CommentReference> GetAll(CommentReferenceFilter filter);
	}
}
