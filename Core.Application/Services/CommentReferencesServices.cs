using Core.Application.DTOs.CommentReferences;
using Core.Application.DTOs.Email;
using Core.Application.DTOs.Email.TempleteViewModels;
using Core.Application.Interfaces.Helpers;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
using Core.Application.Mappings;
using Core.Application.QueryFilters;
using Core.Application.Wrappers;
using Core.Domain.Entities;
using Core.Domain.Settings;
using Microsoft.Extensions.Options;
using System.Net;

namespace Core.Application.Services
{
	public class CommentReferencesServices: BaseServices<CommentReference, CommentReferenceDTO, SaveCommentReferenceDTO>, ICommentReferencesServices
    {
		private readonly ICommentReferencesRepository repo;
		private readonly IUserServices userServices;
		private readonly IEmailServices emailServices;
		private readonly PersonalInformationOfAdmin personalInformationOfAdmin;

		public CommentReferencesServices(ICommentReferencesRepository repo, IUserServices userServices, IEmailServices emailServices, IOptions<PersonalInformationOfAdmin> PersonalInformationOfAdmin)
            : base(repo)
		{
			this.repo = repo;
			this.userServices = userServices;
			this.emailServices = emailServices;
			personalInformationOfAdmin = PersonalInformationOfAdmin.Value;
		}

		public override async Task<AppResponse<CommentReferenceDTO>> CreateAsync(SaveCommentReferenceDTO saveDto)
		{
			var createResponse = await base.CreateAsync(saveDto);

			if (createResponse.Data is null)
				return createResponse;

			var emailRequest = new EmailRequestDTO(personalInformationOfAdmin.Email, "Confirmar Commentario");
			var emailResult = await emailServices.SendEmailAsync(emailRequest, $"Confirmar el sigueinte comentario: {createResponse.Data.Id}");
			if (emailResult)
				return createResponse.AddError(AppError.Create("Hubo un problema al enviar el correo para confirmar el commentario"));

			return createResponse;
		}

		public override async Task<AppResponse<CommentReferenceDTO>> UpdateAsync(SaveCommentReferenceDTO saveDto, Guid Id)
		{
			var createUpdate = await base.UpdateAsync(saveDto, Id);

			if (createUpdate.Data is null)
				return createUpdate;

			var emailRequest = new EmailRequestDTO(personalInformationOfAdmin.Email, "Confirmar Commentario");
			var emailResult = await emailServices.SendEmailAsync(emailRequest, $"Confirmar el sigueinte comentario: {createUpdate.Data.Id}");
			if (emailResult)
				return createUpdate.AddError(AppError.Create("Hubo un problema al enviar el correo para confirmar el commentario"));

			return createUpdate;
		}

		public async Task<AppResponse<Empty>> ConfirmCommentReferenceAsync(Guid Id)
		{
			var commentReference = await repo.GetByIdAsNoTrackingAsync(Id);
			if (commentReference is null)
				AppError.Create($"No existe un comment reference con el id: {Id}")
					.BuildResponse<Empty>(HttpStatusCode.BadRequest)
					.Throw();

			commentReference!.IsConfirmed = true;
			var result = await repo.UpdateAsync(commentReference);
			if (result) 
				AppError.Create("Hubo un problema al cambiar el estado de confirmacion del comentario")
					.BuildResponse<Empty>(HttpStatusCode.InternalServerError)
					.Throw();

			var userResponse = await userServices.GetByIdAsync(commentReference.AccountId);
			if (userResponse.Data is null)
				AppError.Create("El usuario asociado al comentario no fue encontrado")
					.BuildResponse<Empty>(HttpStatusCode.InternalServerError)
					.Throw();

			var emailRequest = new EmailRequestDTO(userResponse.Data!.Email, "Confirmación del comentario de referencia.");
			var commentReferenceEmail = new CommentReferenceEmail(userResponse.Data.FirstName);
			var emailResult = await emailServices.SendTemplateAsync(emailRequest, "CommentReferenceEmail", commentReferenceEmail);
			if (emailResult)
				AppError.Create("Hubo un problema al enviar el correo de confirmacion")
					.BuildResponse<Empty>(HttpStatusCode.InternalServerError)
					.Throw();

			return new(HttpStatusCode.OK);
		}

		public AppResponse<List<CommentReferenceDTO>> GetAll(CommentReferenceFilter filter)
		{
			var data = repo.GetAll(filter).ToList();
			if (data is null || !data.Any())
				return new(HttpStatusCode.NoContent, "No hay elementos para mostrar");

			var dataDto = Mapper.Map<CommentReferenceDTO, CommentReference>(data);
			if (dataDto is null)
				AppError.Create("Hubo problemas al mappear la request")
					.BuildResponse<CommentReferenceDTO>(HttpStatusCode.InternalServerError)
					.Throw();

			return new(dataDto, HttpStatusCode.OK);
		}
	}
}
