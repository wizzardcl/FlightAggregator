using FlightsAggregator.Shared;
using System;

namespace FlightsAggregator.Services.Implementations.SearchProviders;

public static class SearchViewModelExtensions
{
	public static bool CanUseProvider(this SearchViewModel @this, Guid id)
	{
		return @this.ProviderId.HasValue && @this.ProviderId.Value != id;
	}
}