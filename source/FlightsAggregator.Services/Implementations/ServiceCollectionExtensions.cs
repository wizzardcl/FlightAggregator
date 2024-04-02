using FlightsAggregator.Services.Implementations.BookingProviders;
using FlightsAggregator.Services.Implementations.SearchProviders;
using FlightsAggregator.Services.Implementations.TicketsServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FlightsAggregator.Services.Implementations;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddServices(this IServiceCollection @this, IConfigurationManager configuration)
	{
		@this.AddTransient<ITicketsService, LoggingTicketsService>();
		@this.AddTransient<ITicketsServiceLoggingProvider, CacheSearchService>();
		@this.AddTransient<ITicketsServiceCacheProvider, TicketsService>();
		@this.Configure<TicketsServiceOptions>(configuration.GetSection("SearchService"));
		@this.AddTransient<ICache, InMemoryCache>();

		@this.AddTransient<ILufthansaService, LufthansaService>();

		return @this.AddSearchProviders().AddBookingProviders();
	}
}