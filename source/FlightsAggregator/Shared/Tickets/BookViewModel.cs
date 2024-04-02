using System;

namespace FlightsAggregator.Shared.Tickets;

public sealed class BookViewModel : IProviderViewModel
{
	public Guid ProviderId { get; set; }
	public Guid ResultId { get; set; }

	Guid? IProviderViewModel.ProviderId => ProviderId;
}
