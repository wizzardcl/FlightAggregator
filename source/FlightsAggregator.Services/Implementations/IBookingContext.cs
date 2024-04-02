using FlightsAggregator.Shared.Tickets;
using System;

namespace FlightsAggregator.Services.Implementations;

public interface IBookingContext : IProviderViewModel
{
	Guid ResultId { get; }

	void Booked();
	void Failed(string message);
}