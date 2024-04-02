﻿namespace FlightsAggregator.Shared.Tickets;

public sealed class BookResultViewModel
{
	public bool Success { get; set; }
	public string Message { get; set; } = string.Empty;
}