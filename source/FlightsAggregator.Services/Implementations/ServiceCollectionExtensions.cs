using FlightsAggregator.Services.Implementations.BookingProviders;
using FlightsAggregator.Services.Implementations.SearchProviders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FlightsAggregator.Services.Implementations;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddServices(this IServiceCollection @this, IConfigurationManager configuration)
	{
		@this.AddTransient<ISearchService, SearchService>();
		@this.Configure<SearchServiceOptions>(configuration.GetSection("SearchService"));

		return @this.AddSearchProviders().AddBookingProviders();
	}
}