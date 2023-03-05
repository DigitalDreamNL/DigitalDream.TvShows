using System.ComponentModel.DataAnnotations;

namespace DigitalDream.TvShows.Web.Requests;

public class CreateTvShowRequest
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Language { get; set; } = string.Empty;
    [Required]
    public DateTime Premiered { get; set; }
    [Required] public List<string> Genres { get; set; } = new();
    public string? Summary { get; set; }
}