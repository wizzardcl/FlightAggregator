using FlightsAggregator.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightsAggregator.Services.Implementations.SearchProviders;

public sealed class TimeoutSearchProvider : ISearchProvider
{
	public Guid Id { get; } = Guid.Parse("9ea114de-4920-4b71-aff8-a1f637f8107d");

	public string Name => "Timeout Search";

	public async Task<IEnumerable<SearchItemViewModel>> Search(SearchViewModel model)
	{
		if (model.CanUseProvider(Id)) return Enumerable.Empty<SearchItemViewModel>();

		await Task.Delay(50000);

		return Enumerable.Empty<SearchItemViewModel>();
	}
}
