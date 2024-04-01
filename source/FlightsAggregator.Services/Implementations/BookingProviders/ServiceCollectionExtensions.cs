using Microsoft.Extensions.DependencyInjection;

namespace FlightsAggregator.Services.Implementations.BookingProviders;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddBookingProviders(this IServiceCollection @this)
	{
		@this.AddTransient<IBookingProvider, RandomBookingProvider>();

		return @this;
	}
}