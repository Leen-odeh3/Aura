using Aura.Domain.DTOs.Follow;
using Aura.Domain.Entities;
using AutoMapper;

namespace Aura.Api.Mapping;
public class FollowProfile : Profile
{
    public FollowProfile()
    {
        CreateMap<Follow, FollowResponseDto>();
        CreateMap<FollowRequestDto, Follow>();
    }
}