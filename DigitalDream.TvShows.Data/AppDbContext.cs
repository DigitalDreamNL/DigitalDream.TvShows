using System.Text.Json;
using DigitalDream.TvShows.Entities;
using Microsoft.EntityFrameworkCore;

namespace DigitalDream.TvShows.Data;

public class AppDbContext : DbContext
{
    public DbSet<TvShow> TvShows { get; set; } = null!;

    private string DbPath { get; }

    public AppDbContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "tvshows.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<TvShow>()
            .Property(e => e.Genres)
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null!), 
                v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null!)!);
    }
}