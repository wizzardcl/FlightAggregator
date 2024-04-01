using FlightsAggregator.Services.Implementations.SearchProviders;
using FlightsAggregator.Shared;
using System.Threading.Tasks;

namespace FlightsAggregator.Services.Implementations.BookingProviders;

public sealed class RandomBookingProvider : IBookingProvider
{
	public Task<bool> Book(BookViewModel model)
	{
		if (!model.CanUseProvider(RandomSearchProvider.IdValue)) return Task.FromResult(false);

		return Task.FromResult(true);
	}
}
