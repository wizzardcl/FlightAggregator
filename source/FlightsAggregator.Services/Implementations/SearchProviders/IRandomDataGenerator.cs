using FlightsAggregator.Shared;
using System.Collections.Generic;

namespace FlightsAggregator.Services.Implementations.SearchProviders;

public interface IRandomDataGenerator
{
	IEnumerable<SearchItemViewModel> Generate(SearchViewModel model);
}
