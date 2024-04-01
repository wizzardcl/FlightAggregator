using FlightsAggregator.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightsAggregator.Services.Implementations;

public sealed class SearchService : ISearchService
{
	private readonly IEnumerable<ISearchProvider> _providers;

	public SearchService(IEnumerable<ISearchProvider> providers)
	{
		_providers = providers.ToArray();
	}

	public IEnumerable<ProviderViewModel> GetProviders()
	{
		return _providers.Select(e => new ProviderViewModel { Id = e.Id }).ToList();
	}

	public async Task<SearchResultViewModel> Search(SearchViewModel model)
	{
		var result = new SearchResultViewModel();

		foreach (var provider in _providers)
		{
			if (provider.Id != model.ProviderId) continue;

			result = new SearchResultViewModel
			{
				ProviderId = provider.Id,
				ProviderName = provider.Name,

				Items = (await provider.Search(model)).ToArray()
			};

			break;
		}

		return result;
	}
}
