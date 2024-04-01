using FlightsAggregator.Shared;
using System.Threading.Tasks;

namespace FlightsAggregator.Services.Implementations;

public interface IBookingProvider
{
	Task<bool> Book(BookViewModel model);
}