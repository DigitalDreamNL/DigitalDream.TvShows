using System.Text.Json;
using DigitalDream.TvShows.Data.Repositories;
using DigitalDream.TvShows.Scraper.Models;

namespace DigitalDream.TvShows.Scraper.TvMazeScraper;

public class TvMazeScraper : ITvMazeScraper
{
    private readonly ITvShowRepository _repo;
    private readonly HttpClient _client;
    
    public TvMazeScraper(ITvShowRepository repo, IHttpClientFactory factory)
    {
        _repo = repo;
        _client = factory.CreateClient();
    }

    public async Task Execute()
    {
        var page = await DetermineLastScrapedPage();
        await ScrapePage(page);
    }

    private async Task<int> DetermineLastScrapedPage()
    {
        var highestId = await _repo.GetHighestId();
        return CalculatePageFromLastSeenId(highestId);
    }

    private static int CalculatePageFromLastSeenId(int id)
    {
        return Convert.ToInt32(Math.Floor(Convert.ToDouble(id) / 250));
    }

    private async Task ScrapePage(int page)
    {
        try
        {
            var tvShows = await FetchTvShows(page);
            if (tvShows == null)
                return;

            var relevantTvShows = tvShows.Select(tvShow => tvShow.ToTvShow())
                .Where(tvShow => tvShow.Premiered >= DateTime.Parse("2014-01-01"))
                .ToList();

            if (relevantTvShows.Any())
            {
                await _repo.AddRangeAsync(relevantTvShows);
            }

            Console.WriteLine($"Scraped page {page}; added {relevantTvShows.Count()} shows");

            Thread.Sleep(1000); // TODO: Smarter rate limiting

            await ScrapePage(page+1);
        }
        catch (Exception)
        {
            // TODO: For now Assume rate limited and wait extra long
            Thread.Sleep(60000);
        }
    }

    private async Task<List<TvMazeTvShow>?> FetchTvShows(int page)
    {
        var result = await _client.GetAsync($"https://api.tvmaze.com/shows?page={page}");
        result.EnsureSuccessStatusCode();
        return await ParseResult(result);
    }

    private static async Task<List<TvMazeTvShow>?> ParseResult(HttpResponseMessage result)
    {
        var json = await result.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<TvMazeTvShow>>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
    }
}