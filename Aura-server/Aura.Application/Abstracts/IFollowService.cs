using Aura.Domain.DTOs.Follow;
namespace Aura.Application.Abstracts;
public interface IFollowService
{
    Task<FollowResponseDto> FollowUserAsync(FollowRequestDto followRequestDto);
}