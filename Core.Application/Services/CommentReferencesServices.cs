using Core.Application.DTOs.CommentReferences;
using Core.Application.Interfaces.Helpers;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
using Core.Domain.Entities;

namespace Core.Application.Services
{
    public class CommentReferencesServices: BaseServices<CommentReferences, CommentReferencesDTO, SaveCommentReferencesDTO>, ICommentReferencesServices
    {
        public CommentReferencesServices(ICommentReferencesRepository repo, IMapper Mapper)
            : base(repo, Mapper)
        {}
    }
}
