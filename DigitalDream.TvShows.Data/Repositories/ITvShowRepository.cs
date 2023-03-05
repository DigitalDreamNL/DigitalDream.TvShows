using DigitalDream.TvShows.Entities;

namespace DigitalDream.TvShows.Data.Repositories;

public interface ITvShowRepository
{
    public Task<List<TvShow>> ListAsync();
    public Task<TvShow> AddAsync(TvShow tvShow);
    public Task<List<TvShow>> AddRangeAsync(List<TvShow> tvShows);
    public Task<TvShow?> GetByIdAsync(int id);
    public Task<TvShow> UpdateAsync(TvShow tvShow);
    public Task<int> GetHighestId();
}