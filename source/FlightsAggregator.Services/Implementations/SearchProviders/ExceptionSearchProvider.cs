using FlightsAggregator.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightsAggregator.Services.Implementations.SearchProviders;

public sealed class ExceptionSearchProvider : ISearchProvider
{
	public Guid Id { get; } = Guid.Parse("72c6eadb-b911-41c1-b06f-9adcde2ae307");
	public string Name => "Exception Search Provider";

	public Task<IEnumerable<SearchItemViewModel>> Search(SearchViewModel model)
	{
		if (model.CanUseProvider(Id)) return Task.FromResult(Enumerable.Empty<SearchItemViewModel>());

		throw new Exception($"An exception from '{nameof(ExceptionSearchProvider)}'.");
	}
}
