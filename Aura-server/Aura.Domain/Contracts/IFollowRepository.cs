using Aura.Domain.DTOs.User;
using Aura.Domain.Entities;

namespace Aura.Domain.Contracts;
public interface IFollowRepository
{
    Task<Follow> AddFollowAsync(int followerId, int followingId);
    Task<Follow> RemoveFollowAsync(int followerId, int followingId);
    Task<List<UserResponseDto>> GetFollowersAsync(int userId);
    Task<List<UserResponseDto>> GetFollowingAsync(int userId);
}