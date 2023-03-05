using AutoMapper;
using DigitalDream.TvShows.Data.Repositories;
using DigitalDream.TvShows.Entities;
using DigitalDream.TvShows.Web.Controllers;
using DigitalDream.TvShows.Web.Requests;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace DigitalDream.TvShows.Web.Tests.Controllers;

public class TvShowControllerTests
{
    private readonly Mock<ITvShowRepository> _repo = new();
    // TODO: To mock or not to mock Mapper?
    private readonly Mapper _mapper = new (new MapperConfiguration(expression => expression.AddProfile(new MappingProfile())));
    private readonly TvShowController _controller;

    public TvShowControllerTests()
    {
        _controller = new TvShowController(_repo.Object, _mapper);
    }

    [Fact]
    public async Task GivenListOfTvShows_WhenCallingList_AllTvShowsAreReturned()
    {
        var tvShows = TvShowFixture.CreateDefaultTvShows();
        _repo.Setup(r => r.ListAsync()).ReturnsAsync(tvShows);

        var result = await _controller.List() as OkObjectResult;

        result?.StatusCode.Should().Be(StatusCodes.Status200OK);
        var tvShowDTOs = TvShowFixture.CreateDefaultTvShowDTOs();
        result?.Value.Should().BeEquivalentTo(tvShowDTOs);
    }

    [Fact]
    public async Task GivenValidInput_WhenCallingCreate_TvShowIsAdded()
    {
        var request = CreateDefaultCreateTvShowRequest();

        var result = await _controller.Create(request) as OkObjectResult;

        _repo.Verify(r => r.AddAsync(It.Is<TvShow>(s => s.AsJsonString()  == request.AsJsonString())), Times.Once);

        result?.StatusCode.Should().Be(StatusCodes.Status200OK);
        result?.Value.Should().BeEquivalentTo(new TvShow
        {
            Id = request.Id,
            Name = request.Name,
            Language = request.Language,
            Premiered = request.Premiered,
            Genres = request.Genres,
            Summary = request.Summary
        });
    }

    [Fact]
    public async Task GivenIdAlreadyTaken_WhenCallingCreate_BadRequestIsReturned()
    {
        _repo.Setup(r => r.GetByIdAsync(It.Is<int>(id => id == 42))).ReturnsAsync(new TvShow());

        var request = CreateDefaultCreateTvShowRequest();

        var result = await _controller.Create(request) as BadRequestResult;

        result?.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
    }

    [Fact]
    public async Task GivenValidRequest_WhenCallingUpdate_TvShowIsUpdated()
    {
        var request = CreateDefaultUpdateTvShowRequest();
        var updatedTvShow = new TvShowBuilder().WithDefaultValues().Build();
        _repo.Setup(r => r.GetByIdAsync(It.Is<int>(id => id == 1))).ReturnsAsync(new TvShow());
        _repo.Setup(r => r.UpdateAsync(It.Is<TvShow>(s => s.AsJsonString() == updatedTvShow.AsJsonString())))
            .ReturnsAsync(updatedTvShow);

        var result = await _controller.Update(1, request) as OkObjectResult;

        _repo.Verify(r => r.UpdateAsync(It.Is<TvShow>(s => s.AsJsonString() == updatedTvShow.AsJsonString())), Times.Once);
        result?.StatusCode.Should().Be(StatusCodes.Status200OK);
        result?.Value.Should().BeEquivalentTo(updatedTvShow);
    }

    [Fact]
    public async Task GivenRequestForNonExistentTvShow_WhenCallingUpdate_NotFoundIsReturned()
    {
        var request = CreateDefaultUpdateTvShowRequest();
        _repo.Setup(r => r.GetByIdAsync(It.Is<int>(id => id == 42))).Returns(Task.FromResult<TvShow>(null!)!);

        var result = await _controller.Update(1, request) as NotFoundResult;

        result?.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        _repo.Verify(r => r.UpdateAsync(It.IsAny<TvShow>()), Times.Never);
    }

    private static CreateTvShowRequest CreateDefaultCreateTvShowRequest()
    {
        return new CreateTvShowRequest
        {
            Id = 42,
            Name = "Breaking Bad",
            Language = "English",
            Premiered = DateTime.Parse("2023-03-05"),
            Genres = new List<string> { "Drama", "Crime", "Thriller" },
            Summary = "Breaking Bad follows protagonist Walter White, a chemistry teacher who lives in New Mexico with his wife and teenage son who has cerebral palsy. White is diagnosed with Stage III cancer and given a prognosis of two years left to live. With a new sense of fearlessness based on his medical prognosis, and a desire to secure his family's financial security, White chooses to enter a dangerous world of drugs and crime and ascends to power in this world."
        };
    }

    private static UpdateTvShowRequest CreateDefaultUpdateTvShowRequest()
    {
        return new UpdateTvShowRequest
        {
            Name = "Breaking Bad",
            Language = "English",
            Premiered = DateTime.Parse("2023-03-05"),
            Genres = new List<string> { "Drama", "Crime", "Thriller" },
            Summary = "Breaking Bad follows protagonist Walter White, a chemistry teacher who lives in New Mexico with his wife and teenage son who has cerebral palsy. White is diagnosed with Stage III cancer and given a prognosis of two years left to live. With a new sense of fearlessness based on his medical prognosis, and a desire to secure his family's financial security, White chooses to enter a dangerous world of drugs and crime and ascends to power in this world."
        };
    }
}
