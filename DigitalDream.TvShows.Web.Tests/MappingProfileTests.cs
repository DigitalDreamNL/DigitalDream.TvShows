using AutoMapper;

namespace DigitalDream.TvShows.Web.Tests;

public class MappingProfileTests
{
    [Fact]
    public void AutomapperProfileTest()
    {
        new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()))
            .AssertConfigurationIsValid();
    }
}