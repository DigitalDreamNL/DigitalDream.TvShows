using AutoMapper;
using DigitalDream.TvShows.Entities;
using DigitalDream.TvShows.Web.Models;

namespace DigitalDream.TvShows.Web;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TvShow, TvShowDTO>()
            .ReverseMap();
    }
}