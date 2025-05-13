using Core.Application.DTOs.CommentReferences;
using Core.Domain.Entities;

namespace Core.Application.Interfaces.Services
{
    public interface ICommentReferencesServices : IBaseServices<CommentReferences, CommentReferencesDTO, SaveCommentReferencesDTO>
    {}
}
