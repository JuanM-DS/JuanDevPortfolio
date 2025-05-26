using Core.Application.DTOs.ProjectImage;
using FluentValidation;

namespace Core.Application.Validations
{
	public class SaveProjectImageValidator : AbstractValidator<SaveProjectImageDTO>
	{
		public SaveProjectImageValidator()
		{
			RuleFor(x => x.ProjectId)
				.NotEmpty().WithMessage("El ProjectId no puede estar vacío.");

			RuleFor(x => x)
				.Must(x => x.ImageFile != null || !string.IsNullOrWhiteSpace(x.ImageUrl))
				.WithMessage("Debe proporcionar una imagen o una URL.");

			When(x => !string.IsNullOrWhiteSpace(x.ImageUrl), () =>
			{
				RuleFor(x => x.ImageUrl)
					.Must(url => Uri.TryCreate(url, UriKind.Absolute, out var result) &&
								 (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps))
					.WithMessage("La URL de la imagen no es válida.");
			});

			When(x => x.ImageFile != null, () =>
			{
				RuleFor(x => x.ImageFile!.Length)
					.LessThanOrEqualTo(5 * 1024 * 1024) 
					.WithMessage("La imagen no debe superar los 5 MB.");

				RuleFor(x => x.ImageFile!.ContentType)
					.Must(contentType => new[] { "image/jpeg", "image/png", "image/webp" }.Contains(contentType))
					.WithMessage("El tipo de imagen debe ser JPEG, PNG o WEBP.");
			});
		}
	}
}
