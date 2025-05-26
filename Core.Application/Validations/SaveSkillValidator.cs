using Core.Application.DTOs.Skill;
using FluentValidation;

namespace Core.Application.Validations
{
	public class SaveSkillValidator : AbstractValidator<SaveSkillDTO>
	{
		public SaveSkillValidator()
		{
			RuleFor(x => x.Title)
				.NotEmpty().WithMessage("El título de la habilidad no puede estar vacío.")
				.MinimumLength(3).WithMessage("El título debe tener al menos 3 caracteres.")
				.MaximumLength(100).WithMessage("El título no puede exceder los 100 caracteres.");

			RuleFor(x => x.Descripcion)
				.NotEmpty().WithMessage("La descripción no puede estar vacía.")
				.MinimumLength(10).WithMessage("La descripción debe tener al menos 10 caracteres.")
				.MaximumLength(1000).WithMessage("La descripción no puede exceder los 1000 caracteres.");

			RuleFor(x => x.ProfileId)
				.NotEmpty().WithMessage("El ProfileId no puede estar vacío.");

			RuleFor(x => x.TechnologyItems)
				.NotNull().WithMessage("La lista de tecnologías no puede ser nula.")
				.Must(list => list.Count > 0).WithMessage("Debe agregar al menos una tecnología.");

			When(x => x.TechnologyItems.Any(), () =>
			{
				RuleForEach(x => x.TechnologyItems)
					.SetValidator(new SaveTechnologyItemValidator());
			});
		}
	}
}
