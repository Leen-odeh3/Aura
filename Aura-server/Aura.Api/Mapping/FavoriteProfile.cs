using Aura.Domain.DTOs.Favorite;
using Aura.Domain.Entities;
using AutoMapper;

namespace Aura.Api.Mapping;
public class FavoriteProfile : Profile
{
    public FavoriteProfile()
    {
        CreateMap<Favorite, FavoriteResponseDto>()
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.Username))
            .ForMember(dest => dest.Post, opt => opt.MapFrom(src => src.Post));

        CreateMap<FavoriteRequestDto, Favorite>()
            .ForMember(dest => dest.PostId, opt => opt.MapFrom(src => src.PostId));
    }
}
