using System.Net;
using DigitalDream.TvShows.Data.Repositories;
using Moq;
using Moq.Protected;

namespace DigitalDream.TvShows.Scraper.Tests;

public class TvMazeScraperTests
{
    [Fact(Skip="Does not work yet")]
    public async Task Test()
    {
        var repo = new Mock<ITvShowRepository>();
        var factory = new Mock<IHttpClientFactory>();

        var handler = new Mock<HttpMessageHandler>();
        handler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage {
                StatusCode = HttpStatusCode.NotFound,
                Content = new StringContent("")
            });
        factory.Setup(f => f.CreateClient()).Returns(new HttpClient(handler.Object));

        var scraper = new TvMazeScraper.TvMazeScraper(repo.Object, factory.Object);

        await scraper.Execute();
        
        // TODO: Assert records written to database
    }

    // TODO: Add tests for non-happy flows
}