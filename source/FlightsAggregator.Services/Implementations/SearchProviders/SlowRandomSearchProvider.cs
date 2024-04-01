using FlightsAggregator.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightsAggregator.Services.Implementations.SearchProviders;

public sealed class SlowRandomSearchProvider : ISearchProvider
{
	public Guid Id { get; } = Guid.Parse("f0f3e814-f8c8-42dc-ad4c-1404ae4bf2fc");
	public string Name => "Slow Random";

	public async Task<IEnumerable<SearchItemViewModel>> Search(SearchViewModel model)
	{
		if (model.CanUseProvider(Id)) return Enumerable.Empty<SearchItemViewModel>();

		var result = new List<SearchItemViewModel>();
		var random = new Random(DateTime.Now.Millisecond);

		await Task.Delay(random.Next(1000, 1000 + random.Next(2000)));

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
