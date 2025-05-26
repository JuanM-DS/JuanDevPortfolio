using Core.Application.DTOs.TTechnologyItem;
using FluentValidation;

namespace Core.Application.Validations
{
	public class SaveTechnologyItemValidator : AbstractValidator<SaveTechnologyItemDTO>
	{
		public SaveTechnologyItemValidator()
		{
			RuleFor(x => x.Name)
				.NotEmpty().WithMessage("El nombre de la tecnología es requerido.")
				.MinimumLength(2).WithMessage("El nombre debe tener al menos 2 caracteres.")
				.MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres.");

			RuleFor(x => x.Description)
				.NotEmpty().WithMessage("La descripción de la tecnología es requerida.")
				.MinimumLength(5).WithMessage("La descripción debe tener al menos 5 caracteres.")
				.MaximumLength(500).WithMessage("La descripción no puede exceder los 500 caracteres.");

			RuleFor(x => x)
				.Must(x => x.ImageFile != null || !string.IsNullOrWhiteSpace(x.ImageIconUrl))
				.WithMessage("Debe proporcionar un archivo de imagen o una URL de imagen.");

			When(x => !string.IsNullOrWhiteSpace(x.ImageIconUrl), () =>
			{
				RuleFor(x => x.ImageIconUrl)
					.Must(url => Uri.TryCreate(url, UriKind.Absolute, out var result) &&
								 (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps))
					.WithMessage("La URL de la imagen no es válida.");
			});

			When(x => x.ImageFile != null, () =>
			{
				RuleFor(x => x.ImageFile!.Length)
					.LessThanOrEqualTo(2 * 1024 * 1024) // 2 MB
					.WithMessage("La imagen no debe superar los 2 MB.");

				RuleFor(x => x.ImageFile!.ContentType)
					.Must(type => new[] { "image/jpeg", "image/png", "image/webp" }.Contains(type))
					.WithMessage("Solo se permiten imágenes JPEG, PNG o WEBP.");
			});

			RuleFor(x => x.LevelType)
				.IsInEnum().WithMessage("El nivel especificado no es válido.");
		}
	}
}
