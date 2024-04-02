using FlightsAggregator.Services.Implementations;
using FlightsAggregator.Services.Implementations.TicketsServices;
using FlightsAggregator.Services.Validators;
using FlightsAggregator.Shared.Tickets;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestsFlightsAggregator.Services.Implementations.TicketsServices;

[TestFixture]
public sealed class TicketsServiceTests
{
	private TicketsService Target { get; set; }
	private IOptions<TicketsServiceOptions> Options { get; set; }
	private ILogger<TicketsService> Logger { get; set; }
	private List<ISearchProvider> SearchProviders { get; set; }
	private List<IBookingProvider> BookingProviders { get; set; }
	private SearchViewModelValidator Validator { get; set; }

	[SetUp]
	public void SetUp()
	{
		Options = Substitute.For<IOptions<TicketsServiceOptions>>();
		Logger = Substitute.For<ILogger<TicketsService>>();
		SearchProviders = new List<ISearchProvider>();
		BookingProviders = new List<IBookingProvider>();
		Validator = new SearchViewModelValidator();

		Options.Value.Returns(new TicketsServiceOptions());

		Target = new TicketsService(Options, Logger, SearchProviders, BookingProviders, Validator);
	}

	[Test]
	public async Task should_return_data_from_providers()
	{
		//arrange
		var model = new SearchViewModel
		{
			From = Airport.List.First().Value.Code,
			To = Airport.List.Last().Value.Code,
			Time = DateTime.UtcNow.AddDays(10).Date
		};
		SearchItemViewModel[] items1 =
		[
			new SearchItemViewModel(),
			new SearchItemViewModel()
		], items2 = [new SearchItemViewModel(), new SearchItemViewModel(), new SearchItemViewModel()];

		const string provider1Name = "P1 N", provider2Name = "P2 N";
		Guid p1Id = Guid.NewGuid(), p2Id = Guid.NewGuid();
		ISearchProvider provider1 = Substitute.For<ISearchProvider>(), provider2 = Substitute.For<ISearchProvider>();

		provider1.Name.Returns(provider1Name);
		provider1.Id.Returns(p1Id);
		provider1.Search(model).Returns(items1);

		provider2.Search(model).Returns(items2);
		provider2.Name.Returns(provider2Name);
		provider2.Id.Returns(p2Id);

		SearchProviders.Add(provider1);
		SearchProviders.Add(provider2);
		//act
		var actual = await Target.Search(model);
		//assert
		Assert.Multiple(() =>
		{
			Assert.That(actual.Select(e => new { id = e.ProviderId, name = e.ProviderName }),
				Is.EquivalentTo(new[] { new { id = p1Id, name = provider1Name }, new { id = p2Id, name = provider2Name } }));
			Assert.That(actual.SelectMany(e => e.Items), Is.EquivalentTo(items1.Union(items2)));
		});
	}

	[Test]
	public async Task should_handle_exceptions()
	{
		//arrange
		var model = new SearchViewModel
		{
			From = Airport.List.First().Value.Code,
			To = Airport.List.Last().Value.Code,
			Time = DateTime.UtcNow.AddDays(10).Date
		};

		var provider = Substitute.For<ISearchProvider>();

		var exception = new Exception("message");
		provider.Search(model).Throws(exception);
		SearchProviders.Add(provider);
		//act
		var actual = await Target.Search(model);
		//assert
		Assert.That(actual.SelectMany(e => e.Items), Is.Empty);
	}
}
