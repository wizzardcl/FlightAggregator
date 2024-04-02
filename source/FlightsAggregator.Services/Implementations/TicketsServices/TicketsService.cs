using FlightsAggregator.Shared;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightsAggregator.Services.Implementations.TicketsServices;

public sealed class TicketsService : ITicketsServiceCacheProvider
{
	private readonly IEnumerable<ISearchProvider> _providers;
	private readonly IOptions<TicketsServiceOptions> _options;
	private readonly ILogger<TicketsService> _logger;
	private readonly IEnumerable<IBookingProvider> _bookingProviders;

	public TicketsService(IOptions<TicketsServiceOptions> options, ILogger<TicketsService> logger,
			IEnumerable<ISearchProvider> searchProviders, IEnumerable<IBookingProvider> bookingProviders)
	{
		_providers = searchProviders.ToArray();
		_options = options;
		_logger = logger;
		_bookingProviders = bookingProviders;
	}

	public async Task<BookResultViewModel> Book(BookViewModel model)
	{
		var context = new BookingContext(model);
		foreach (var booking in _bookingProviders)
		{
			await booking.Book(context);
			if (context.Handled) return new BookResultViewModel { Success = context.Success, Message = context.Message };
		}

		return new BookResultViewModel { Success = false, Message = "An issue with book your request." };
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

	private sealed class BookingContext : IBookingContext
	{
		private readonly BookViewModel _model;

		public Guid ResultId => _model.ResultId;
		public Guid? ProviderId => _model.ProviderId;
		public bool Handled { get; private set; }
		public bool Success { get; private set; } = true;
		public string Message { get; private set; } = string.Empty;

		public BookingContext(BookViewModel model)
		{
			_model = model;
		}

		public void Booked()
		{
			Handled = true;
			Message = "Booked successfully.";
		}

		public void Failed(string message)
		{
			Handled = true;
			Success = false;
			Message = message;
		}
	}
}
