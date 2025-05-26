using Core.Application.DTOs.CommentReferences;
using FluentValidation;

namespace Core.Application.Validations
{
	public class CommentReferencesValidations : AbstractValidator<SaveCommentReferenceDTO>
	{
		public CommentReferencesValidations()
		{
			RuleFor(x => x.AccountId)
				.NotEmpty().WithMessage("AccountId no puede ser nulo.")
				.NotNull().WithMessage("AccountId no puede estar vacío.");

			RuleFor(x => x.Comment)
				.NotEmpty().WithMessage("El comentario no puede estar vacío.")
				.NotNull().WithMessage("El comentario no puede ser nulo.")
				.MinimumLength(3).WithMessage("El comentario debe tener al menos 3 caracteres.")
				.MaximumLength(500).WithMessage("El comentario no puede exceder los 500 caracteres.");

			RuleFor(x => x.ProfileId)
				.NotEmpty().WithMessage("ProfileId no puede estar vacío.")
				.NotNull().WithMessage("ProfileId no puede ser nulo.");
		}
	}
}
