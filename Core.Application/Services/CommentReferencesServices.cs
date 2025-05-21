using Core.Application.DTOs.CommentReferences;
using Core.Application.Interfaces.Helpers;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
using Core.Application.QueryFilters;
using Core.Application.Wrappers;
using Core.Domain.Entities;
using System.Net;

namespace Core.Application.Services
{
    public class CommentReferencesServices: BaseServices<CommentReference, CommentReferenceDTO, SaveCommentReferenceDTO>, ICommentReferencesServices
    {
		private readonly ICommentReferencesRepository repo;

		public CommentReferencesServices(ICommentReferencesRepository repo, IMapper Mapper)
            : base(repo, Mapper)
		{
			this.repo = repo;
		}

		public AppResponse<List<CommentReferenceDTO>> GetAll(CommentReferenceFilter filter)
		{
			var data = repo.GetAll(filter).ToList();
			if (data is null || !data.Any())
				return new(HttpStatusCode.NoContent, "No hay elementos para mostrar");

			var dataDto = _mapper.Map<CommentReferenceDTO, CommentReference>(data);
			if (dataDto is null)
				AppError.Create("Hubo problemas al mappear la request")
					.BuildResponse<CommentReferenceDTO>(HttpStatusCode.InternalServerError)
					.Throw();

			return new(dataDto, HttpStatusCode.OK);
		}
	}
}
