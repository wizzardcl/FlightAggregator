using FlightsAggregator.Shared;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightsAggregator.Services.Implementations;

public sealed class SearchService : ISearchService
{
	private readonly IEnumerable<ISearchProvider> _providers;
	private readonly IOptions<SearchServiceOptions> _options;
	private readonly ILogger<SearchService> _logger;
	private readonly IEnumerable<IBookingProvider> _bookingProviders;

	public SearchService(IOptions<SearchServiceOptions> options, ILogger<SearchService> logger,
		IEnumerable<ISearchProvider> searchProviders, IEnumerable<IBookingProvider> bookingProviders)
	{
		_providers = searchProviders.ToArray();
		_options = options;
		_logger = logger;
		_bookingProviders = bookingProviders;
	}

	public async Task<BookResultViewModel> Book(BookViewModel model)
	{
		foreach (var booking in _bookingProviders)
		{
			var result = await booking.Book(model);
			if (result) return new BookResultViewModel();
		}

		return new BookResultViewModel { Success = false, Message = "Item Not Found" };
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
					if (searchResultTask.Wait(_options.Value.Timeout)) return new SearchResultViewModel(providerClosing.Id, providerClosing.Name, searchResultTask.Result.ToArray());
				}
				catch (Exception e)
				{
					_logger.LogError(e, $"Error in '{providerClosing.Id}'.");

					return new SearchResultViewModel(providerClosing.Id, providerClosing.Name, "Error");
				}

				_logger.LogError($"Time out for provider '{providerClosing.Id}'.");

				return new SearchResultViewModel(providerClosing.Id, providerClosing.Name, "Timeout");
			}));
		}

		var result = await Task.WhenAll(tasks);

		return result.ToArray();
	}
}
