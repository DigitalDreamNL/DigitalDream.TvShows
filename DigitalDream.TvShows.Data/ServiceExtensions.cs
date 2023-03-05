using DigitalDream.TvShows.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DigitalDream.TvShows.Data;

public static class ServiceExtensions
{
    public static void AddDatabaseContextAndRepositories(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>();
        services.AddScoped<ITvShowRepository, TvShowRepository>();
    }
}