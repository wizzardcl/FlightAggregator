using FlightsAggregator.Shared.Lufthansa;
using System;
using System.Collections.Generic;

namespace FlightsAggregator.Services.Implementations;

public sealed class LufthansaService : ILufthansaService
{
	public IEnumerable<FlyInfoViewModel> GetTickets(string from, string to, DateTime date)
	{
		var random = new Random(DateTime.Now.Millisecond);
		var departure = date.Date.AddMinutes(random.Next(200));

		return new[]
		{
			new FlyInfoViewModel
			{
				Id = Guid.Parse("97e23751-314d-472d-ad10-54bff0f99069"),
				Departure = departure,
				Arrival = departure.AddMinutes(random.Next(200)),
				Cost = 100
			},

			new FlyInfoViewModel
			{
				Id = Guid.Parse("97e23751-314d-472d-ad10-54bff0f99065"),
				Departure = date.AddMinutes(random.Next(200)),
				Arrival = date.AddMinutes(random.Next(200, 500)),
				Cost = 100
			}
		};
	}
}