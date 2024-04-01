using FlightsAggregator.Shared;
using System;
using System.Collections.Generic;

namespace FlightsAggregator.Services.Implementations.SearchProviders;

public sealed class RandomDataGenerator : IRandomDataGenerator
{
	public IEnumerable<SearchItemViewModel> Generate(SearchViewModel model)
	{
		var result = new List<SearchItemViewModel>();
		var random = new Random(DateTime.Now.Millisecond);

		for (var i = 0; i < random.Next(2, 10); i++)
		{
			var departure = model.Time.Date.AddMinutes(random.Next(720));

			result.Add(new SearchItemViewModel
			{
				Airline = "Britich Airlines",
				Departure = departure,
				Arrival = departure.AddMinutes(random.Next(720)),
			});
		}

		return result;
	}
}