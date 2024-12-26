
using Aura.Domain.DTOs.Favorite;
using Aura.Domain.Entities;

namespace Aura.Domain.Contracts;
public interface IFavoriteRepository
{
    Task<List<FavoriteResponseDto>> GetFavoritesByUserIdAsync(int userId);
    Task<List<FavoriteResponseDto>> GetFavoritesByPostIdAsync(int postId);
    Task<FavoriteResponseDto> AddFavoriteAsync(int postId, int userId);
    Task<FavoriteResponseDto> RemoveFavoriteAsync(int favoriteId);
}