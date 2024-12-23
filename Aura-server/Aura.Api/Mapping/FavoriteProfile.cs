using Aura.Domain.DTOs.Favorite;
using Aura.Domain.Entities;
using AutoMapper;

namespace Aura.Api.Mapping;
public class FavoriteProfile : Profile
{
    public FavoriteProfile()
    {
        CreateMap<FavoriteRequestDto, Favorite>()
            .ForMember(dest => dest.PostId, opt => opt.MapFrom(src => src.PostId))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => DateTime.UtcNow));

        CreateMap<Favorite, FavoriteResponseDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => src.DateCreated))
            .ForMember(dest => dest.PostId, opt => opt.MapFrom(src => src.PostId))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));
    }
}