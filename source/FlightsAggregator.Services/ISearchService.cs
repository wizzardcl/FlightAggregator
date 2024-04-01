using FlightsAggregator.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightsAggregator.Services;

public interface ISearchService
{
	IEnumerable<ProviderViewModel> GetProviders();
	Task<SearchResultViewModel> Search(SearchViewModel model);
}
