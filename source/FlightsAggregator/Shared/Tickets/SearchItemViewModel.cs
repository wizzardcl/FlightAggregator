using System;

namespace FlightsAggregator.Shared.Tickets;

public sealed class SearchItemViewModel
{
	public Guid ResultId { get; set; }
	public string Airline { get; set; } = string.Empty;
	public DateTime Departure { get; set; }
	public DateTime Arrival { get; set; }
	public int Cost { get; set; }
}
