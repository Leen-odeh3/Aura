using Aura.Domain.DTOs.User;
using Aura.Domain.Entities;
using AutoMapper;

namespace Aura.Api.Mapping;
public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserResponseDto>();

        CreateMap<UpdateAboutRequest, User>()
      .ForMember(dest => dest.About, opt => opt.MapFrom(src => src.NewAbout));
    }
}