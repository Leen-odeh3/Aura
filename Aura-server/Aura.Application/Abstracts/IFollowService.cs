using Aura.Domain.DTOs.User;
using Aura.Domain.Entities;

namespace Aura.Application.Abstracts;
public interface IFollowService
{
    Task FollowUserAsync(int followerId, int followingId);
    Task UnfollowUserAsync(int followerId, int followingId);
    Task<List<UserResponseDto>> GetFollowersAsync(int userId);
    Task<List<UserResponseDto>> GetFollowingAsync(int userId);
}