using Aura.Application.Abstracts;
using Aura.Domain.Contracts;
using Aura.Domain.DTOs.User;
using Aura.Domain.Entities;

namespace Aura.Application.Services;
public class FollowService : IFollowService
{
    private readonly IFollowRepository _followRepository;

    public FollowService(IFollowRepository followRepository)
    {
        _followRepository = followRepository;
    }

    public async Task FollowUserAsync(int followerId, int followingId)
    {
        await _followRepository.AddFollowAsync(followerId, followingId);
    }

    public async Task UnfollowUserAsync(int followerId, int followingId)
    {
        await _followRepository.RemoveFollowAsync(followerId, followingId);
    }

    public async Task<List<UserResponseDto>> GetFollowersAsync(int userId)
    {
        return await _followRepository.GetFollowersAsync(userId);
    }

    public async Task<List<UserResponseDto>> GetFollowingAsync(int userId)
    {
        return await _followRepository.GetFollowingAsync(userId);
    }
}