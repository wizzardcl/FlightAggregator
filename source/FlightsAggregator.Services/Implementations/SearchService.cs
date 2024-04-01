using FlightsAggregator.Shared;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightsAggregator.Services.Implementations;

public sealed class SearchService : ISearchService
{
	private readonly IEnumerable<ISearchProvider> _providers;
	private readonly ILogger<SearchService> _logger;

	public SearchService(IEnumerable<ISearchProvider> providers, ILogger<SearchService> logger)
	{
		_providers = providers.ToArray();
		_logger = logger;
	}

	public IEnumerable<ProviderViewModel> GetProviders()
	{
		return _providers.Select(e => new ProviderViewModel { Id = e.Id }).ToList();
	}

	public async Task<SearchResultViewModel[]> Search(SearchViewModel model)
	{
		var tasks = new List<Task<SearchResultViewModel>>();

		foreach (var provider in _providers)
		{
			var providerClosing = provider;

			tasks.Add(Task.Run(() =>
			{
				try
				{
					var searchResultTask = providerClosing.Search(model);
					if (searchResultTask.Wait(5000))
						return new SearchResultViewModel
						{
							Items = searchResultTask.Result.ToArray(),
							ProviderId = providerClosing.Id,
							ProviderName = providerClosing.Name
						};
				}
				catch (Exception e)
				{
					_logger.LogError(e, $"Error in '{providerClosing.Id}'.");

					return new SearchResultViewModel
					{
						Message = "Error",
						Success = false,

						ProviderId = providerClosing.Id,
						ProviderName = providerClosing.Name
					};
				}

				_logger.LogError($"Time out for provider '{providerClosing.Id}'.");

				return new SearchResultViewModel
				{
					Message = "Timeout",
					Success = false,

					ProviderId = providerClosing.Id,
					ProviderName = providerClosing.Name
				};
			}));
		}

		var result = await Task.WhenAll(tasks);

		return result.ToArray();
	}
}
