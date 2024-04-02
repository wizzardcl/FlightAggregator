using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace FlightsAggregator.Services.Implementations;

public sealed class InMemoryCache : ICache
{
	private readonly IMemoryCache _cache;

	public InMemoryCache(IMemoryCache cache)
	{
		_cache = cache;
	}

	public async Task<T> Get<T>(string key, Func<Task<T>> factory)
	{
		if (_cache.Get(key) is T result) return result;

		result = await factory();

		_cache.Set(key, result, DateTimeOffset.Now.AddDays(1));

		return result;
	}
}