using FlightsAggregator.Shared;
using System;

namespace FlightsAggregator.Services.Implementations.SearchProviders;

public static class ProviderViewModelExtensions
{
	public static bool CanUseProvider(this IProviderViewModel @this, Guid id)
	{
		return !@this.ProviderId.HasValue || @this.ProviderId.Value == id;
	}
}