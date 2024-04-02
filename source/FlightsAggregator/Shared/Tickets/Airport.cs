using System.Collections.Generic;

namespace FlightsAggregator.Shared.Tickets;

public sealed class Airport
{
	public string Code { get; }
	public string Name { get; }

	public Airport(string code, string name)
	{
		Code = code;
		Name = name;
	}

	public static Dictionary<string, Airport> List { get; } = new Dictionary<string, Airport>
	{
		{"BER", new Airport("BER", "Berlin (Brandenburg)") },
		{"HHN", new Airport("HHN", "Hahn (Rhineland-Palatinate)") },
		{"MAD", new Airport("MAD", "Madrid (Madrid)") }
	};
}
