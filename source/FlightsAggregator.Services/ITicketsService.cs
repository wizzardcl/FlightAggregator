using FlightsAggregator.Shared.Tickets;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightsAggregator.Services;

public interface ITicketsService
{
	Task<BookResultViewModel> Book(BookViewModel model);
	IEnumerable<ProviderViewModel> GetProviders();
	Task<SearchResultViewModel[]> Search(SearchViewModel model);
}
