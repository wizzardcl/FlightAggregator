using FlightsAggregator.Services.Implementations.SearchProviders;
using System.Threading.Tasks;

namespace FlightsAggregator.Services.Implementations.BookingProviders;

public sealed class RandomBookingProvider : IBookingProvider
{
	public Task Book(IBookingContext cnt)
	{
		if (!cnt.CanUseProvider(RandomSearchProvider.IdValue) && !cnt.CanUseProvider(SlowRandomSearchProvider.IdValue)) return Task.CompletedTask;

		cnt.Booked();

		return Task.CompletedTask;
	}
}
