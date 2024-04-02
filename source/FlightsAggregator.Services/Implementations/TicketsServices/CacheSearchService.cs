using FlightsAggregator.Shared.Tickets;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightsAggregator.Services.Implementations.TicketsServices;

public sealed class CacheSearchService : ITicketsServiceLoggingProvider
{
	private readonly ITicketsServiceCacheProvider _provider;
	private readonly ICache _cache;

	public CacheSearchService(ITicketsServiceCacheProvider provider, ICache cache)
	{
		_provider = provider;
		_cache = cache;
	}

	public Task<BookResultViewModel> Book(BookViewModel model)
	{
		return _provider.Book(model);
	}

	public IEnumerable<ProviderViewModel> GetProviders()
	{
		return _provider.GetProviders();
	}

	public async Task<SearchResultViewModel[]> Search(SearchViewModel model)
	{
		var key = $"{model.From}/{model.To}/{model.Time:yyyy-MM-dd}/{model.ProviderId}";

		return await _cache.Get(key, async () => await _provider.Search(model));
	}
}
