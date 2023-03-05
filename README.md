# DigitalDream.TvShows

A Web API and Scraper to find recent TV shows on TVmaze.

## Getting started

Run the Entity Framework migration to set up the database:

```
dotnet ef database update --startup-project .\DigitalDream.TvShows.Web\DigitalDream.TvShows.Web.csproj --project .\DigitalDream.TvShows.Data\DigitalDream.TvShows.Data.csproj
```

Then run either the DigitalDream.TvShows.Web application for the Web API or DigitalDream.TvShows.Scraper for the Scraper.