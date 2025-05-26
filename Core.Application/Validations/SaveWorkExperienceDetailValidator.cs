using Core.Application.DTOs.ExperienceDetail;
using FluentValidation;

namespace Core.Application.Validations
{
	public class SaveWorkExperienceDetailValidator : AbstractValidator<SaveWorkExperienceDetailDTO>
	{
		public SaveWorkExperienceDetailValidator()
		{
			RuleFor(x => x.Title)
				.NotEmpty().WithMessage("El título es requerido.")
				.MinimumLength(3).WithMessage("El título debe tener al menos 3 caracteres.")
				.MaximumLength(100).WithMessage("El título no debe exceder los 100 caracteres.");

			RuleFor(x => x.Descripcion)
				.NotEmpty().WithMessage("La descripción es requerida.")
				.MinimumLength(5).WithMessage("La descripción debe tener al menos 5 caracteres.")
				.MaximumLength(500).WithMessage("La descripción no debe exceder los 500 caracteres.");

			RuleFor(x => x.ExperienceId)
				.NotEmpty().WithMessage("El ID de experiencia es requerido.");
		}
	}
}
