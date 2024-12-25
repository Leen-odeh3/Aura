using Aura.Domain.DTOs.User;
using Aura.Domain.Entities;
using AutoMapper;

namespace Aura.Api.Mapping;
public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserResponseDto>()
                    .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image));

        CreateMap<UpdateAboutRequest, User>()
      .ForMember(dest => dest.About, opt => opt.MapFrom(src => src.NewAbout));
    }
}