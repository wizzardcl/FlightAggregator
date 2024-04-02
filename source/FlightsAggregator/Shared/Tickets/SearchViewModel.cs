using System;

namespace FlightsAggregator.Shared.Tickets;

public sealed class SearchViewModel : IProviderViewModel
{
	public Guid? ProviderId { get; set; }
	public string From { get; set; }
	public string To { get; set; }
	public DateTime Time { get; set; } = DateTime.Now;
}
