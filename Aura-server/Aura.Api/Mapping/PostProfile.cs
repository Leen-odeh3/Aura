using Aura.Domain.DTOs.Image;
using Aura.Domain.DTOs.Post;
using Aura.Domain.DTOs.User;
using Aura.Domain.Entities;
using AutoMapper;

namespace Aura.Api.Mapping;
public class PostProfile : Profile
{
    public PostProfile()
    {
        CreateMap<Post, PostResponseDto>()
            .ForMember(dest => dest.Image, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => new UserResponseDto
            {
                Id = src.User.Id,
                Username = src.User.Username,
                About = src.User.About,
                Image = src.User.Image == null ? null : new ImageResponseDto
                {
                    Id = src.User.Image.Id,
                    ImagePath = src.User.Image.ImagePath
                }
            }));

        CreateMap<CreatePost, Post>()
            .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.DateUpdated, opt => opt.MapFrom(src => DateTime.UtcNow));

        CreateMap<PostRemove, Post>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PostId));

        CreateMap<PostRepost, Post>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PostId));

        CreateMap<PostVisibility, Post>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PostId));
    }
}