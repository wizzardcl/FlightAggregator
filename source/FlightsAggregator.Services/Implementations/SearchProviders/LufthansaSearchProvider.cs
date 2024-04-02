using FlightsAggregator.Shared;
using FlightsAggregator.Shared.Lufthansa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace FlightsAggregator.Services.Implementations.SearchProviders;

public sealed class LufthansaSearchProvider : ISearchProvider
{
	private readonly IHttpClientFactory _factory;

	public Guid Id { get; } = Guid.Parse("97e23751-314d-472d-ad10-54bff0f99069");
	public string Name => "Lufthansa (Http Call)";

	public LufthansaSearchProvider(IHttpClientFactory factory)
	{
		_factory = factory;
	}

	public async Task<IEnumerable<SearchItemViewModel>> Search(SearchViewModel model)
	{
		if (!model.CanUseProvider(Id)) return Enumerable.Empty<SearchItemViewModel>();

		var client = _factory.CreateClient();

		var response = await client.GetAsync($"http://localhost:5274/api/lufthansa?from={model.From}&to={model.To}&date={model.Time:yyyy-MM-dd}");

		response.EnsureSuccessStatusCode();

		var data = await response.Content.ReadAsStringAsync();
		var items = JsonSerializer.Deserialize<FlyInfoViewModel[]>(data, new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true
		});

		return items.Select(e => new SearchItemViewModel { Cost = e.Cost, Airline = "Lufthansa", Arrival = e.Arrival, Departure = e.Departure, ResultId = e.Id });
	}
}
