using System;

namespace FlightsAggregator.Shared.Tickets;

public sealed class SearchResultViewModel
{
	public Guid ProviderId { get; set; }
	public string ProviderName { get; set; } = string.Empty;

	public bool Success { get; set; } = true;
	public string Message { get; set; } = string.Empty;

	public SearchItemViewModel[] Items { get; set; } = Array.Empty<SearchItemViewModel>();

	public SearchResultViewModel() { }

	public SearchResultViewModel(Guid providerId, string providerName, string errorMessage)
	{
		ProviderId = providerId;
		ProviderName = providerName;
		Message = errorMessage;
		Success = false;
	}

	public SearchResultViewModel(Guid providerId, string providerName, SearchItemViewModel[] items)
	{
		ProviderId = providerId;
		ProviderName = providerName;
		Items = items;
	}
}
