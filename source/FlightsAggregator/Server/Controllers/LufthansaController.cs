using FlightsAggregator.Services;
using FlightsAggregator.Shared.Lufthansa;
using Microsoft.AspNetCore.Mvc;

namespace FlightsAggregator.Server.Controllers;

/// <summary>
/// Test controller for Lufthansa provider
/// </summary>
[ApiController]
[Route("api/[controller]")]
public sealed class LufthansaController : ControllerBase
{
	private readonly ILufthansaService _service;

	public LufthansaController(ILufthansaService service)
	{
		_service = service;
	}

	[HttpGet]
	public IEnumerable<FlyInfoViewModel> GetAll(string from, string to, DateTime date)
	{
		return _service.GetTickets(from, to, date);
	}
}