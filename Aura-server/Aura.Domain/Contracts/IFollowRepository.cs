using Aura.Domain.DTOs.User;
using Aura.Domain.Entities;

namespace Aura.Domain.Contracts;
public interface IFollowRepository
{
    Task FollowUserAsync(int followerId, int followedId);
    Task<bool> IsFollowingAsync(int followerId, int followedId);

}