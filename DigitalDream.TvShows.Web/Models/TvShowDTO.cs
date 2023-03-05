namespace DigitalDream.TvShows.Web.Models;

public class TvShowDTO
{
    public int Id { get; set; }
    public string Name { get; set; }  = string.Empty;
    public string Language { get; set; } = string.Empty;
    public DateTime Premiered { get; set; }
    public List<string> Genres { get; set; } = new();
    public string? Summary { get; set; }
}