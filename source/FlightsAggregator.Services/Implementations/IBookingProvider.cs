using System.Threading.Tasks;

namespace FlightsAggregator.Services.Implementations;

public interface IBookingProvider
{
	Task Book(IBookingContext cnt);
}
