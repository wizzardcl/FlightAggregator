using System;
using System.Threading.Tasks;

namespace FlightsAggregator.Services.Implementations;

public interface ICache
{
	Task<T> Get<T>(string key, Func<Task<T>> factory);
}
