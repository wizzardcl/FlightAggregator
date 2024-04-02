using Microsoft.Extensions.DependencyInjection;

namespace FlightsAggregator.Services.Implementations.SearchProviders;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddSearchProviders(this IServiceCollection @this)
	{
		@this.AddTransient<ISearchProvider, RandomSearchProvider>();
		@this.AddTransient<ISearchProvider, SlowRandomSearchProvider>();
		@this.AddTransient<ISearchProvider, TimeoutSearchProvider>();
		@this.AddTransient<ISearchProvider, ExceptionSearchProvider>();
		@this.AddTransient<ISearchProvider, InFileSearchProvider>();
		@this.AddTransient<IRandomDataGenerator, RandomDataGenerator>();
		@this.AddTransient<ISearchProvider, LufthansaSearchProvider>();

		return @this;
	}
}