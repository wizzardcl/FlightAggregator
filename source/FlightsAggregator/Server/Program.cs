using FlightsAggregator.Services;
using FlightsAggregator.Services.Implementations;
using FlightsAggregator.Services.Implementations.SearchProviders;
using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddTransient<ISearchService, SearchService>();
builder.Services.AddTransient<ISearchProvider, RandomSearchProvider>();
builder.Services.AddTransient<ISearchProvider, SlowRandomSearchProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseWebAssemblyDebugging();
}
else
{
	app.UseExceptionHandler("/Error");
}

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
