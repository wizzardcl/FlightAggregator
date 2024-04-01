﻿using FlightsAggregator.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightsAggregator.Services.Implementations.SearchProviders;

public sealed class RandomSearchProvider : ISearchProvider
{
	public Guid Id { get; } = Guid.Parse("59e29e05-2b38-4645-bc4e-8b8ab026d969");
	public string Name => "Random";

	public Task<IEnumerable<SearchItemViewModel>> Search(SearchViewModel model)
	{
		if (model.CanUseProvider(Id)) return Task.FromResult(Enumerable.Empty<SearchItemViewModel>());

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

		return Task.FromResult<IEnumerable<SearchItemViewModel>>(result);
	}
}
