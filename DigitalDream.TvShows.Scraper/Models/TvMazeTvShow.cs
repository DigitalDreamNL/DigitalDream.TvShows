using DigitalDream.TvShows.Entities;

namespace DigitalDream.TvShows.Scraper.Models;

public class TvMazeTvShow
{
    public string Name { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
    public string Premiered { get; set; } = string.Empty;
    public List<string> Genres { get; set; } = new();
    public string? Summary { get; set; }
}

public static class TvMazeTvShowExtensions
{
    public static TvShow ToTvShow(this TvMazeTvShow tvShowSource)
    {
        DateTime.TryParse(tvShowSource.Premiered, out var premiered);

        return new TvShow
        {
            Name = tvShowSource.Name,
            Genres = tvShowSource.Genres,
            Language = tvShowSource.Language,
            Premiered = premiered,
            Summary = tvShowSource.Summary,
        };
    }
}
