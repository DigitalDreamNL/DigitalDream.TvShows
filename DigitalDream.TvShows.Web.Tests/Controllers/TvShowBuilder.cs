using DigitalDream.TvShows.Entities;

namespace DigitalDream.TvShows.Web.Tests.Controllers;

public class TvShowBuilder
{
    private readonly TvShow _tvShow = new ();

    public TvShowBuilder Id(int id)
    {
        _tvShow.Id = id;
        return this;
    }

    public TvShowBuilder Name(string name)
    {
        _tvShow.Name = name;
        return this;
    }

    public TvShowBuilder WithDefaultValues()
    {
        _tvShow.Id = 1;
        _tvShow.Name = "Breaking Bad";
        _tvShow.Language = "English";
        _tvShow.Premiered = DateTime.Parse("2023-03-05");
        _tvShow.Genres = new List<string> { "Drama", "Crime", "Thriller" };
        _tvShow.Summary = "Breaking Bad follows protagonist Walter White, a chemistry teacher who lives in New Mexico with his wife and teenage son who has cerebral palsy. White is diagnosed with Stage III cancer and given a prognosis of two years left to live. With a new sense of fearlessness based on his medical prognosis, and a desire to secure his family's financial security, White chooses to enter a dangerous world of drugs and crime and ascends to power in this world.";
        return this;
    }

    public TvShow Build() => _tvShow;
}
