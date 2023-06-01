using FluentValidation;
using Inventory.Domain.Utils;

namespace Inventory.Domain.Entities.Validators;

public class CompanyValidator : AbstractValidator<Company>
{
	public CompanyValidator()
	{
		RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("{PropertyName} is required")
			.MinimumLength(3).WithMessage("Minimum length must be {MinLength} characters")
			.MaximumLength(100).WithMessage("Maximum length must be {MaxLength} characters");

		RuleFor(x => x.Document).NotNull().NotNull().WithMessage("{PropertyName} is required")
			.Custom((document, context) =>
			{
				if (!DocumentUtils.IsValidDocument(document))
					context.AddFailure(
						"Document must be a valid data");
			});
	}
}
