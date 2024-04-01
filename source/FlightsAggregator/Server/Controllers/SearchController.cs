using FlightsAggregator.Services;
using FlightsAggregator.Shared;
using Microsoft.AspNetCore.Mvc;

namespace FlightsAggregator.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class SearchController : ControllerBase
{
	private readonly ISearchService _service;

	public SearchController(ISearchService service)
	{
		_service = service;
	}

	[HttpGet("get-for/{from}/{to}/{time:datetime}/{providerId:guid?}")]
	public async Task<SearchResultViewModel[]> Search(Guid? providerId, string from, string to, DateTime time)
	{
		return await _service.Search(new SearchViewModel { ProviderId = providerId, From = from, To = to, Time = time });
	}

	[HttpGet("get-providers")]
	public IEnumerable<ProviderViewModel> GetProviders()
	{
		return _service.GetProviders();
	}

	[HttpPost]
	public async Task<BookResultViewModel> Book(BookViewModel model)
	{
		return await _service.Book(model);
	}
}
