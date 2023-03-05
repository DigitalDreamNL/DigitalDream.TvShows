// See https://aka.ms/new-console-template for more information

using DigitalDream.TvShows.Data;
using DigitalDream.TvShows.Scraper;
using DigitalDream.TvShows.Scraper.TvMazeScraper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

Console.WriteLine("TV Maze scraper");

var builder = Host.CreateDefaultBuilder();

builder.ConfigureServices(services =>
{
    services.AddDatabaseContextAndRepositories();
    services.AddTransient<ITvMazeScraper, TvMazeScraper>();
    services.AddTransient<IHttpClientFactory, HttpClientFactory>();
});

var app = builder.Build();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    var tvShowScraper = services.GetRequiredService<ITvMazeScraper>();
    await tvShowScraper.Execute();
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}
