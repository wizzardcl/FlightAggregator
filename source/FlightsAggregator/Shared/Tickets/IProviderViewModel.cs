using System;

namespace FlightsAggregator.Shared.Tickets;

public interface IProviderViewModel
{
	Guid? ProviderId { get; }
}
