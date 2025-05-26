using Core.Application.DTOs.Project;
using FluentValidation;

namespace Core.Application.Validations
{
	public class SaveProjectValidations : AbstractValidator<SaveProjectDTO>
	{
		public SaveProjectValidations()
		{
			RuleFor(x => x.Title)
				.NotEmpty().WithMessage("El título del proyecto no puede estar vacío.")
				.NotNull().WithMessage("El título del proyecto es requerido.")
				.MinimumLength(3).WithMessage("El título debe tener al menos 3 caracteres.")
				.MaximumLength(100).WithMessage("El título no puede exceder los 100 caracteres.");

			RuleFor(x => x.Description)
				.NotEmpty().WithMessage("La descripción del proyecto no puede estar vacía.")
				.NotNull().WithMessage("La descripción del proyecto es requerida.")
				.MinimumLength(10).WithMessage("La descripción debe tener al menos 10 caracteres.")
				.MaximumLength(50).WithMessage("La descripción no puede exceder los 1000 caracteres.");

			RuleFor(x => x.GitHubRepositoryUrl)
				.NotEmpty().WithMessage("La URL del repositorio de GitHub no puede estar vacía.")
				.Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out var result) &&
							 (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps))
				.WithMessage("La URL del repositorio de GitHub debe ser válida.");

			RuleFor(x => x.TechnologyItems)
				.NotNull().WithMessage("La lista de tecnologías no puede ser nula.")
				.Must(list => list.Count > 0).WithMessage("Debe agregar al menos una tecnología.");

			RuleFor(x => x.ProjectImages)
				.NotNull().WithMessage("La lista de imágenes no puede ser nula.")
				.Must(list => list.Count > 0).WithMessage("Debe agregar al menos una imagen.");
		}
	}
}
