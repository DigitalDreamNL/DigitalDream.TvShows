using AutoMapper;
using DigitalDream.TvShows.Data.Repositories;
using DigitalDream.TvShows.Entities;
using DigitalDream.TvShows.Web.Models;
using DigitalDream.TvShows.Web.Requests;
using Microsoft.AspNetCore.Mvc;

namespace DigitalDream.TvShows.Web.Controllers;

[ApiController]
public class TvShowController : Controller
{
    private readonly ITvShowRepository _repo;
    private readonly IMapper _mapper;

    public TvShowController(ITvShowRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    [HttpGet("/api/tvshows")]
    public async Task<IActionResult> List()
    {
        var tvShows = await _repo.ListAsync();

        var result = _mapper.Map<List<TvShowDTO>>(tvShows);

        return Ok(result);
    }

    // TODO: Should we let users determine the ID?
    [HttpPost("/api/tvshows")]
    public async Task<IActionResult> Create(CreateTvShowRequest request)
    {
        var existingTvShow = await _repo.GetByIdAsync(request.Id);

        if (existingTvShow != null)
            return BadRequest();

        var tvShow = new TvShow
        {
            Id = request.Id,
            Name = request.Name,
            Language = request.Language,
            Premiered = request.Premiered,
            Genres = request.Genres,
            Summary = request.Summary
        };

        await _repo.AddAsync(tvShow);

        return Ok(tvShow);
    }

    [HttpPatch("/api/tvshows/{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateTvShowRequest request)
    {
        var existingTvShow = await _repo.GetByIdAsync(id);

        if (existingTvShow == null)
            return NotFound();

        var tvShow = await _repo.UpdateAsync(new TvShow
        {
            Id = id,
            Name = request.Name,
            Language = request.Language,
            Premiered = request.Premiered,
            Genres = request.Genres,
            Summary = request.Summary
        });

        return Ok(tvShow);
    }

    // TODO: Add endpoint to delete
}