using FlightsAggregator.Shared.Lufthansa;
using System;
using System.Collections.Generic;

namespace FlightsAggregator.Services;

public interface ILufthansaService
{
	IEnumerable<FlyInfoViewModel> GetTickets(string from, string to, DateTime date);
}