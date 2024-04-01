using System;

namespace FlightsAggregator.Shared;

public sealed class ProviderViewModel
{
	public Guid Id { get; set; }
}

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

public sealed class SearchItemViewModel
{
	public Guid ResultId { get; set; }
	public string Airline { get; set; } = string.Empty;
	public DateTime Departure { get; set; }
	public DateTime Arrival { get; set; }
	public int Cost { get; set; }
}

public sealed class SearchViewModel : IProviderViewModel
{
	public Guid? ProviderId { get; set; }
	public string From { get; set; }
	public string To { get; set; }
	public DateTime Time { get; set; } = DateTime.Now;
}

public sealed class Airport
{
	public string Code { get; }
	public string Name { get; }

	public Airport(string code, string name)
	{
		Code = code;
		Name = name;
	}

	public static Airport[] List { get; } =
	[
		new Airport("BER", "Berlin (Brandenburg)"),
		new Airport("HHN", "Hahn (Rhineland-Palatinate)"),
		new Airport("MAD", "Madrid (Madrid)")
	];
}

public interface IProviderViewModel
{
	Guid? ProviderId { get; }
}

public sealed class BookViewModel : IProviderViewModel
{
	public Guid ProviderId { get; set; }
	public Guid ResultId { get; set; }

	Guid? IProviderViewModel.ProviderId => ProviderId;
}

public sealed class BookResultViewModel
{
	public bool Success { get; set; }
	public string Message { get; set; } = string.Empty;
}