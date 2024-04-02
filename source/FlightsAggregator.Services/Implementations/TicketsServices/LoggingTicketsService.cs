using FlightsAggregator.Shared;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightsAggregator.Services.Implementations.TicketsServices;

public sealed class LoggingTicketsService : ITicketsService
{
	private readonly ILogger<LoggingTicketsService> _logger;
	private readonly ITicketsServiceLoggingProvider _provider;

	public LoggingTicketsService(ILogger<LoggingTicketsService> logger, ITicketsServiceLoggingProvider provider)
	{
		_logger = logger;
		_provider = provider;
	}

	public async Task<BookResultViewModel> Book(BookViewModel model)
	{
		var requestContent = System.Text.Json.JsonSerializer.Serialize(model);

		var response = await _provider.Book(model);

		var responseContent = System.Text.Json.JsonSerializer.Serialize(response);

		_logger.LogInformation($"For method '{nameof(Book)}' request: '{requestContent}' response: '{responseContent}'.");

		return response;
	}

	public IEnumerable<ProviderViewModel> GetProviders()
	{
		return _provider.GetProviders();
	}

	public async Task<SearchResultViewModel[]> Search(SearchViewModel model)
	{
		var requestContent = System.Text.Json.JsonSerializer.Serialize(model);

		var response = await _provider.Search(model);

		var responseContent = System.Text.Json.JsonSerializer.Serialize(response);

		_logger.LogInformation($"For method '{nameof(Search)}' request: '{requestContent}' response: '{responseContent}'.");

		return response;
	}
}