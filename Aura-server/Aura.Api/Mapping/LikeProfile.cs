using Aura.Domain.DTOs.Like;
using Aura.Domain.Entities;
using AutoMapper;

namespace Aura.Api.Mapping;
public class LikeProfile : Profile
{
    public LikeProfile()
    {
        CreateMap<Like, LikeResponseDto>()
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.Username));

        CreateMap<LikeRequestDto, Like>()
            .ForMember(dest => dest.PostId, opt => opt.MapFrom(src => src.PostId))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));
    }
}