using Core.Application.DTOs.CommentReferences;
using Core.Application.QueryFilters;
using Core.Application.Wrappers;
using Core.Domain.Entities;

namespace Core.Application.Interfaces.Services
{
    public interface ICommentReferencesServices : IBaseServices<CommentReference, CommentReferenceDTO, SaveCommentReferenceDTO>
    {
		public AppResponse<List<CommentReferenceDTO>> GetAll(CommentReferenceFilter filter);

		public Task<AppResponse<Empty>> ConfirmCommentReferenceAsync(Guid Id);
	}
}
