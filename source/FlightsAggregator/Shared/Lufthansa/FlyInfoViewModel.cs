using System;

namespace FlightsAggregator.Shared.Lufthansa;

public sealed class FlyInfoViewModel
{
	public Guid Id { get; set; }
	public DateTime Arrival { get; set; }
	public DateTime Departure { get; set; }
	public int Cost { get; set; }
}
