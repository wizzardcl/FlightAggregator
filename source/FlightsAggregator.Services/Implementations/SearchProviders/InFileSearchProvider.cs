using FlightsAggregator.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace FlightsAggregator.Services.Implementations.SearchProviders;

public sealed class InFileSearchProvider : ISearchProvider
{
	public const string FileName = "Files/InFileSearchProvider.json";
	public static Guid IdValue { get; } = Guid.Parse("29cd6279-2b52-40f9-920a-9ba29a02db22");
	public Guid Id => IdValue;
	public string Name => "In File Search Provider";

	public async Task<IEnumerable<SearchItemViewModel>> Search(SearchViewModel model)
	{
		if (!model.CanUseProvider(Id)) return Enumerable.Empty<SearchItemViewModel>();

		var data = string.Empty;
		using (var reader = File.OpenText(FileName))
		{
			data = await reader.ReadToEndAsync();
		}

		var items = JsonSerializer.Deserialize<Item[]>(data);

		return items.Select(e => new SearchItemViewModel
		{
			ResultId = e.Id,
			Airline = e.Airline,
			Arrival = e.Arrival,
			Departure = e.Departure
		});
	}

	public sealed class Item
	{
		public Guid Id { get; set; }
		public string Airline { get; set; } = string.Empty;
		public DateTime Departure { get; set; }
		public DateTime Arrival { get; set; }
		public int Cost { get; set; }
	}
}