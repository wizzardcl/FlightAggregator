using FlightsAggregator.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightsAggregator.Services.Implementations.SearchProviders;

public sealed class SlowRandomSearchProvider : ISearchProvider
{
	private readonly IRandomDataGenerator _generator;

	public Guid Id { get; } = Guid.Parse("f0f3e814-f8c8-42dc-ad4c-1404ae4bf2fc");
	public string Name => "Slow Random";

	public SlowRandomSearchProvider(IRandomDataGenerator generator)
	{
		_generator = generator;
	}

	public async Task<IEnumerable<SearchItemViewModel>> Search(SearchViewModel model)
	{
		if (!model.CanUseProvider(Id)) return Enumerable.Empty<SearchItemViewModel>();

		var random = new Random(DateTime.Now.Millisecond);
		await Task.Delay(random.Next(1000, 1000 + random.Next(2000)));

		return _generator.Generate(model);
	}
}
