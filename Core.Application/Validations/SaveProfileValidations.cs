using Core.Application.DTOs.Experience;
using Core.Application.DTOs.Profile;
using FluentValidation;

namespace Core.Application.Validations
{
	public class SaveProfileValidations : AbstractValidator<SaveProfileDTO>
	{
		public SaveProfileValidations()
		{
			RuleFor(x => x.ProfesionalTitle)
				.NotEmpty().WithMessage("El título profesional no puede estar vacío.")
				.NotNull().WithMessage("El título profesional es requerido.")
				.MinimumLength(3).WithMessage("El título profesional debe tener al menos 3 caracteres.")
				.MaximumLength(200).WithMessage("El título profesional no puede exceder los 100 caracteres.");

			RuleFor(x => x.Description)
				.NotEmpty().WithMessage("La descripción no puede estar vacía.")
				.NotNull().WithMessage("La descripción es requerida.")
				.MinimumLength(10).WithMessage("La descripción debe tener al menos 10 caracteres.")
				.MaximumLength(500).WithMessage("La descripción no puede exceder los 1000 caracteres.");

			RuleFor(x => x.GitHubRepositoryUrl)
				.NotEmpty().WithMessage("La URL del repositorio de GitHub no puede estar vacía.")
				.Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out var result) &&
							 (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps))
				.WithMessage("La URL del repositorio de GitHub debe ser una URL válida.");

			RuleFor(x => x.LinkedinUrl)
				.NotEmpty().WithMessage("La URL de LinkedIn no puede estar vacía.")
				.Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out var result) &&
							 (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps))
				.WithMessage("La URL de LinkedIn debe ser una URL válida.");

			RuleFor(x => x.CvUrl)
				.NotEmpty().WithMessage("La URL del CV no puede estar vacía.")
				.Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out var result) &&
							 (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps))
				.WithMessage("La URL del CV debe ser una URL válida.");

			RuleFor(x => x.AccountId)
				.NotEmpty().WithMessage("El AccountId no puede estar vacío.")
				.NotNull().WithMessage("El AccountId es requerido.");
		}
	}

	public class SaveWorkExperienceValidator : AbstractValidator<SaveWorkExperienceDTO>
	{
		public SaveWorkExperienceValidator()
		{
			RuleFor(x => x.Position)
				.NotEmpty().WithMessage("El cargo (posición) no puede estar vacío.")
				.MaximumLength(200).WithMessage("El cargo no debe exceder los 100 caracteres.");

			RuleFor(x => x.CompanyName)
				.NotEmpty().WithMessage("El nombre de la empresa no puede estar vacío.")
				.MaximumLength(200).WithMessage("El nombre de la empresa no debe exceder los 100 caracteres.");

			RuleFor(x => x.FromDate)
				.NotEmpty().WithMessage("La fecha de inicio es requerida.")
				.LessThanOrEqualTo(DateTime.Now).WithMessage("La fecha de inicio no puede ser futura.");

			RuleFor(x => x.ToDate)
				.Must((dto, toDate) => toDate == null || toDate >= dto.FromDate)
				.WithMessage("La fecha de finalización debe ser posterior o igual a la fecha de inicio.");

			RuleFor(x => x.Description)
				.NotEmpty().WithMessage("La descripción no puede estar vacía.")
				.MaximumLength(500).WithMessage("La descripción no debe exceder los 1000 caracteres.");

			RuleFor(x => x.ProfileId)
				.NotEmpty().WithMessage("El ProfileId no puede estar vacío.");

			RuleFor(x => x)
				.Must(x => x.LogoFile != null || !string.IsNullOrWhiteSpace(x.CompanyLogoUrl))
				.WithMessage("Debe proporcionar un archivo de logo o una URL de logo.");

			When(x => !string.IsNullOrWhiteSpace(x.CompanyLogoUrl), () =>
			{
				RuleFor(x => x.CompanyLogoUrl)
					.Must(url => Uri.TryCreate(url, UriKind.Absolute, out var result) &&
								 (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps))
					.WithMessage("La URL del logo no es válida.");
			});

			When(x => x.LogoFile != null, () =>
			{
				RuleFor(x => x.LogoFile!.Length)
					.LessThanOrEqualTo(2 * 1024 * 1024).WithMessage("El logo no debe superar los 2 MB.");

				RuleFor(x => x.LogoFile!.ContentType)
					.Must(type => new[] { "image/jpeg", "image/png", "image/webp" }.Contains(type))
					.WithMessage("El tipo de archivo del logo debe ser JPEG, PNG o WEBP.");
			});

			When(x => x.TechnologyItems.Any(), () =>
			{
				RuleForEach(x => x.TechnologyItems)
					.SetValidator(new SaveTechnologyItemValidator());
			});

			When(x => x.ExperienceDetails.Any(), () =>
			{
				RuleForEach(x => x.ExperienceDetails)
					.SetValidator(new SaveWorkExperienceDetailValidator());
			});
		}
	}
}
