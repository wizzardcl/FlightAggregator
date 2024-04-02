using FlightsAggregator.Shared.Tickets;
using FluentValidation;
using System;

namespace FlightsAggregator.Services.Validators;

public sealed class SearchViewModelValidator : AbstractValidator<SearchViewModel>
{
	public SearchViewModelValidator()
	{
		RuleFor(e => e.From).NotNull().Custom(AiportValidation);

		RuleFor(e => e.To).NotNull().Custom(AiportValidation);

		RuleFor(e => e.Time).GreaterThanOrEqualTo(DateTime.Now.Date);
	}

	private void AiportValidation(string aiport, ValidationContext<SearchViewModel> cnt)
	{
		if (Airport.List.ContainsKey(aiport)) return;

		cnt.AddFailure("Unknown airport.");
	}
}
