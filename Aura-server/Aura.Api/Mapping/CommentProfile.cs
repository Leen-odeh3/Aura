using Aura.Domain.DTOs.Comment;
using Aura.Domain.Entities;
using AutoMapper;

namespace Aura.Api.Mapping;
public class CommentProfile : Profile
{
    public CommentProfile()
    {
        CreateMap<PostComment, Comment>()
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
            .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.DateUpdated, opt => opt.MapFrom(src => DateTime.UtcNow));

        CreateMap<Comment, CommentResponseDto>()
              .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.Username));

        CreateMap<RemoveComment, Comment>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CommentId));
    }
}