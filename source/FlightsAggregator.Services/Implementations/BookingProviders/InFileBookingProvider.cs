using FlightsAggregator.Services.Implementations.SearchProviders;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace FlightsAggregator.Services.Implementations.BookingProviders;

public sealed class InFileBookingProvider : IBookingProvider
{
	public async Task Book(IBookingContext cnt)
	{
		if (!cnt.CanUseProvider(InFileSearchProvider.IdValue)) return;

		var data = string.Empty;
		using (var reader = File.OpenText(InFileSearchProvider.FileName))
		{
			data = await reader.ReadToEndAsync();
		}

		var item = JsonSerializer.Deserialize<InFileSearchProvider.Item[]>(data).FirstOrDefault(e => e.Id == cnt.ResultId);
		if (item == null)
		{
			cnt.Failed("Not Found.");
			return;
		}

		if (item.Cost % 10 == 0) cnt.Failed("Test Failed");
		else cnt.Booked();
	}
}