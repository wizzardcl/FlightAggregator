using FlightsAggregator.Shared.Tickets;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightsAggregator.Services.Implementations;

public interface ISearchProvider
{
	Guid Id { get; }
	string Name { get; }

	Task<IEnumerable<SearchItemViewModel>> Search(SearchViewModel model);
}
