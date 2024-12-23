
using Aura.Domain.DTOs.Like;

namespace Aura.Domain.Contracts;
public interface ILikeRepository
{
    Task<List<LikeResponseDto>> GetLikesByPostIdAsync(int postId);
    Task<LikeResponseDto> AddLikeAsync(int postId, int userId);
    Task<LikeResponseDto> RemoveLikeAsync(int likeId);
}