using Aura.Application.Abstracts;
using Aura.Application.Abstracts.FileServices;
using Aura.Domain.Contracts;
using Aura.Domain.DTOs.Like;

namespace Aura.Application.Services;
public class LikeService : ILikeService
{
    private readonly ILikeRepository _likeRepository;

    public LikeService(ILikeRepository likeRepository)
    {
        _likeRepository = likeRepository;
    }

    public async Task<List<LikeResponseDto>> GetLikesByPostIdAsync(int postId)
    {
        return await _likeRepository.GetLikesByPostIdAsync(postId);
    }

    public async Task<LikeResponseDto> AddLikeAsync(int postId, int userId)
    {
        return await _likeRepository.AddLikeAsync(postId, userId);
    }

    public async Task<LikeResponseDto> RemoveLikeAsync(int likeId)
    {
        return await _likeRepository.RemoveLikeAsync(likeId);
    }
}