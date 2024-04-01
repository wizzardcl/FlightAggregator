using FlightsAggregator.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightsAggregator.Services.Implementations.SearchProviders;

public sealed class RandomSearchProvider : ISearchProvider
{
	private readonly IRandomDataGenerator _generator;

	public static Guid IdValue { get; } = Guid.Parse("59e29e05-2b38-4645-bc4e-8b8ab026d969");
	public Guid Id => IdValue;
	public string Name => "Random";

	public RandomSearchProvider(IRandomDataGenerator generator)
	{
		_generator = generator;
	}

	public Task<IEnumerable<SearchItemViewModel>> Search(SearchViewModel model)
	{
		if (!model.CanUseProvider(Id)) return Task.FromResult(Enumerable.Empty<SearchItemViewModel>());

		return Task.FromResult(_generator.Generate(model));
	}
}
