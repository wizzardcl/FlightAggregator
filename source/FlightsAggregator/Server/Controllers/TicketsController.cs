using FlightsAggregator.Services;
using FlightsAggregator.Shared.Tickets;
using Microsoft.AspNetCore.Mvc;

namespace FlightsAggregator.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class TicketsController : ControllerBase
{
	private readonly ITicketsService _service;

	public TicketsController(ITicketsService service)
	{
		_service = service;
	}

	/// <summary>
	/// Search tickets by provided arguments.
	/// </summary>
	/// <param name="providerId">Provider id (Optional).</param>
	/// <param name="from">From (airport)</param>
	/// <param name="to">To (airport)</param>
	/// <param name="date">Date of departure</param>
	/// <returns></returns>
	[HttpGet("get-for/{from}/{to}/{date:datetime}/{providerId:guid?}")]
	public async Task<SearchResultViewModel[]> Search(Guid? providerId, string from, string to, DateTime date)
	{
		return await _service.Search(new SearchViewModel { ProviderId = providerId, From = from, To = to, Time = date });
	}

	/// <summary>
	/// Get all available providers
	/// </summary>
	/// <returns></returns>
	[HttpGet("get-providers")]
	public IEnumerable<ProviderViewModel> GetProviders()
	{
		return _service.GetProviders();
	}

	/// <summary>
	/// Book a ticket
	/// </summary>
	/// <param name="model"></param>
	/// <returns></returns>
	[HttpPost]
	public async Task<BookResultViewModel> Book(BookViewModel model)
	{
		return await _service.Book(model);
	}
}
