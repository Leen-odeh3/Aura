using Aura.Application.Abstracts;
using Aura.Domain.Contracts;
using Aura.Domain.DTOs.Favorite;
namespace Aura.Application.Services;
public class FavoriteService : IFavoriteService
{
    private readonly IFavoriteRepository _favoriteRepository;

    public FavoriteService(IFavoriteRepository favoriteRepository)
    {
        _favoriteRepository = favoriteRepository;
    }

    public async Task<List<FavoriteResponseDto>> GetFavoritesByPostIdAsync(int postId)
    {
        return await _favoriteRepository.GetFavoritesByPostIdAsync(postId);
    }

    public async Task<FavoriteResponseDto> AddFavoriteAsync(int postId, int userId)
    {
        return await _favoriteRepository.AddFavoriteAsync(postId, userId);
    }

    public async Task<FavoriteResponseDto> RemoveFavoriteAsync(int favoriteId)
    {
        return await _favoriteRepository.RemoveFavoriteAsync(favoriteId);
    }
}