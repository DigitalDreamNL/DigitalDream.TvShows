using DigitalDream.TvShows.Entities;

namespace DigitalDream.TvShows.Data;

public class DatabaseSeeder
{
    private readonly AppDbContext _dbContext;
    
    public static readonly TvShow TvShow1 = new()
    {
        Id = 99999,
        Name = "Breaking Bad",
        Language = "English",
        Premiered = DateTime.UtcNow,
        Genres = new List<string> { "Drama", "Crime", "Thriller" },
        Summary = "Breaking Bad follows protagonist Walter White, a chemistry teacher who lives in New Mexico with his wife and teenage son who has cerebral palsy. White is diagnosed with Stage III cancer and given a prognosis of two years left to live. With a new sense of fearlessness based on his medical prognosis, and a desire to secure his family's financial security, White chooses to enter a dangerous world of drugs and crime and ascends to power in this world."
    };

    public DatabaseSeeder(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Execute()
    {
        if (_dbContext.TvShows.Any())
            return;

        await _dbContext.TvShows.AddAsync(TvShow1);

        await _dbContext.SaveChangesAsync();
    }
}