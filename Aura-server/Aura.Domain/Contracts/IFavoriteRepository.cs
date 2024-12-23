
using Aura.Domain.DTOs.Favorite;

namespace Aura.Domain.Contracts;
public interface IFavoriteRepository
{
    Task<List<FavoriteResponseDto>> GetFavoritesByPostIdAsync(int postId);
    Task<FavoriteResponseDto> AddFavoriteAsync(int postId, int userId);
    Task<FavoriteResponseDto> RemoveFavoriteAsync(int favoriteId);
}