using DigitalDream.TvShows.Entities;
using DigitalDream.TvShows.Web.Models;

namespace DigitalDream.TvShows.Web.Tests.Controllers;

public class TvShowFixture
{
    public static List<TvShow> CreateDefaultTvShows()
    {
        return new List<TvShow>
        {
            new()
            {
                Id = 1,
                Name = "Breaking Bad",
                Language = "English",
                Genres = new List<string> { "Drama" },
                Premiered = DateTime.Parse("2023-03-05"),
                Summary = "Summary goes here"
            },
            new()
            {
                Id = 2,
                Name = "Parks and Recreation",
                Language = "English",
                Genres = new List<string> { "Comedy" },
                Premiered = DateTime.Parse("2023-03-05"),
                Summary = "Parks and Rec"
            }
        };
    }

    public static List<TvShowDTO> CreateDefaultTvShowDTOs()
    {
        return new List<TvShowDTO>
        {
            new()
            {
                Id = 1,
                Name = "Breaking Bad",
                Language = "English",
                Genres = new List<string> { "Drama" },
                Premiered = DateTime.Parse("2023-03-05"),
                Summary = "Summary goes here"
            },
            new()
            {
                Id = 2,
                Name = "Parks and Recreation",
                Language = "English",
                Genres = new List<string> { "Comedy" },
                Premiered = DateTime.Parse("2023-03-05"),
                Summary = "Parks and Rec"
            }
        };
    }
}