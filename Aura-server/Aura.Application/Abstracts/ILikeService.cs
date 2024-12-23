using Aura.Domain.DTOs.Like;
namespace Aura.Application.Abstracts;
public interface ILikeService
{
    Task<List<LikeResponseDto>> GetLikesByPostIdAsync(int postId);
    Task<LikeResponseDto> AddLikeAsync(int postId, int userId);
    Task<LikeResponseDto> RemoveLikeAsync(int likeId);
}