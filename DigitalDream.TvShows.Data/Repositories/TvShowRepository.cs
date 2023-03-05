using DigitalDream.TvShows.Data.Exceptions;
using DigitalDream.TvShows.Entities;
using Microsoft.EntityFrameworkCore;

namespace DigitalDream.TvShows.Data.Repositories;

public class TvShowRepository : ITvShowRepository
{
    private readonly AppDbContext _dbContext;

    public TvShowRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<TvShow>> ListAsync()
    {
        return await _dbContext.TvShows
            .OrderByDescending(s => s.Premiered)
            .ToListAsync();
    }

    public async Task<int> GetHighestId()
    {
        var lastTvShow = await _dbContext.TvShows.OrderByDescending(s => s.Id).FirstOrDefaultAsync();
        return lastTvShow?.Id ?? 0;
    }

    public async Task<TvShow> AddAsync(TvShow tvShow)
    {
        await _dbContext.TvShows.AddAsync(tvShow);
        await _dbContext.SaveChangesAsync();
        return tvShow; // TODO: Return actual database records
    }

    public async Task<List<TvShow>> AddRangeAsync(List<TvShow> tvShows)
    {
        await _dbContext.TvShows.AddRangeAsync(tvShows);
        await _dbContext.SaveChangesAsync();
        return tvShows; // TODO: Return actual database records
    }

    public async Task<TvShow?> GetByIdAsync(int id)
    {
        return await _dbContext.TvShows.FindAsync(id);
    }

    public async Task<TvShow> UpdateAsync(TvShow tvShow)
    {
        var record = await _dbContext.TvShows.FindAsync(tvShow.Id);
        if (record == null)
            throw new TvShowNotFoundException();

        record.Id = tvShow.Id;
        record.Name = tvShow.Name;
        record.Language = tvShow.Language;
        record.Premiered = tvShow.Premiered;
        record.Genres = tvShow.Genres;
        record.Summary = tvShow.Summary;

        _dbContext.Update(record);

        await _dbContext.SaveChangesAsync();

        return record;
    }
}